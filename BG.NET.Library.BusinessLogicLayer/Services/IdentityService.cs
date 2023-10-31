using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using BG.NET.Library.BusinessLogicLayer.Interfaces;
using BG.NET.Library.DataAccessLayer.Contexts;
using BG.NET.Library.Models.Configuration;
using BG.NET.Library.Models.Dto.Auth;
using BG.NET.Library.Models.Entities.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BG.NET.Library.BusinessLogicLayer.Services;

public class IdentityService : IIdentityService
{
    private readonly IMapper _mapper;
    private readonly IdentityDbContext _context;
    private readonly ILogger<IdentityService> _logger;
    private readonly IOptions<JwtOptions> _jwtOptions;

    public IdentityService(IMapper mapper, IdentityDbContext context, ILogger<IdentityService> logger, IOptions<JwtOptions> jwtOptions)
    {
        _mapper = mapper;
        _context = context;
        _logger = logger;
        _jwtOptions = jwtOptions;
    }
    public async Task<int?> Register(RegisterDto user)
    {
        if (await _context.Users!.AnyAsync(u => u.Username == user.Username!))
            return null;
        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

        var mappedUser = _mapper.Map<RegisterDto, User>(user);
        var userEntityEntry = await _context.Users!.AddAsync(mappedUser);
        await _context.SaveChangesAsync();
        _logger.LogInformation("User @{EntityUsername} registered (UserId: {EntityId})",
            userEntityEntry.Entity.Username, userEntityEntry.Entity.Id);

        return userEntityEntry.Entity.Id;
    }

    public async Task<JwtTokenDto?> Login(LoginDto user)
    {
        var userExists = await _context.Users!.FirstOrDefaultAsync(u => u.Username == user.Username);
        
        // Credentials validation
        if (userExists == null)
            return null;
        if (!BCrypt.Net.BCrypt.Verify(user.Password, userExists.Password))
        {
            _logger.LogWarning("Unsuccessful authorization attempt for user @{UserExistsUsername} (UserId: {UserExistsId})", userExists.Username, userExists.Id);
            return null;
        }
        var secretKey = _jwtOptions.Value.Secret;
        var issuer = _jwtOptions.Value.Issuer;
        var audience = _jwtOptions.Value.Audience;
        
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, userExists.Username!),
            new Claim(ClaimTypes.NameIdentifier, userExists.Id.ToString())
        };

        var tokenExpiresAt = DateTime.Now.AddHours(1);
        var tokenOptions = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: tokenExpiresAt,
            signingCredentials: signingCredentials
        );
        
        _logger.LogInformation("Successful authorization for user @{UserExistsUsername} (UserId: {UserExistsId}) at {DateTimeNow}", userExists.Username, userExists.Id, DateTime.Now);
        
        return new JwtTokenDto
        {
            Token = new JwtSecurityTokenHandler().WriteToken(tokenOptions),
            Expires = tokenExpiresAt
        };
    }

    public async Task<UserInfoDto?> Info(Claim claim)
    {
        if (claim.Type != ClaimTypes.NameIdentifier) return null;
        var userInfo = await _context.Users!.FirstOrDefaultAsync(u => u.Id == Int32.Parse(claim.Value));
        return userInfo == null
            ? null
            : _mapper.Map<User, UserInfoDto>(userInfo);
    }
}