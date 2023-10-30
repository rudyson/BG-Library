using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using BG.NET.Library.Models.Configuration;
using BG.NET.Library.Models.Dto.Auth;
using BG.NET.Library.Models.Entities.Auth;
using BGLibrary.Identity.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BGLibrary.Identity.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;
    private readonly IdentityDbContext _context;
    private readonly IMapper _mapper;
    private readonly IOptions<JwtOptions> _jwtOptions;

    public AuthController(ILogger<AuthController> logger, IdentityDbContext context, IMapper mapper, IOptions<JwtOptions> jwtOptions)
    {
        _logger = logger;
        _context = context;
        _mapper = mapper;
        _jwtOptions = jwtOptions;
    }
    /// <summary>
    /// Provides user registration, passing RegisterDto model
    /// </summary>
    /// <param name="user">RegisterDto model, which contains required fields to register in system</param>
    /// <returns></returns>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [AllowAnonymous]
    [Route("register")]
    [HttpPost]
    public async Task<IActionResult> Register(RegisterDto user)
    {
        if (!ModelState.IsValid)
            return BadRequest("Passed data is not valid");
        if (await _context.Users!.AnyAsync(u => u.Username == user.Username!))
            return BadRequest("User already exists");
        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

        var mappedUser = _mapper.Map<RegisterDto, User>(user);
        
        var userEntityEntry = await _context.Users!.AddAsync(mappedUser);
        await _context.SaveChangesAsync();
        _logger.LogInformation("User @{EntityUsername} registered (UserId: {EntityId})", userEntityEntry.Entity.Username, userEntityEntry.Entity.Id);

        return Ok("User registered");
    }
    
    /// <summary>
    /// Generates token to authorize user in system
    /// </summary>
    /// <param name="user">Username and password</param>
    /// <returns>JWT Token with 1 hour life duration</returns>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [AllowAnonymous]
    [Route("login")]
    [HttpPost]
    public async Task<IActionResult> Login(LoginDto user)
    {
        // Model validations
        if (!ModelState.IsValid)
            return BadRequest("Passed data is not valid");
        var userExists = await _context.Users!.FirstOrDefaultAsync(u => u.Username == user.Username);
        
        // Credentials validation
        if (userExists == null)
            return NotFound("User is not exists");
        if (!BCrypt.Net.BCrypt.Verify(user.Password, userExists.Password))
        {
            _logger.LogWarning("Unsuccessful authorization attempt for user @{UserExistsUsername} (UserId: {UserExistsId})", userExists.Username, userExists.Id);
            return Unauthorized("Wrong password");
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
        var token = new JwtTokenDto
        {
            Token = new JwtSecurityTokenHandler().WriteToken(tokenOptions),
            Expires = tokenExpiresAt
        };
        
        _logger.LogInformation("Successful authorization for user @{UserExistsUsername} (UserId: {UserExistsId}) at {DateTimeNow}", userExists.Username, userExists.Id, DateTime.Now);
        
        return Ok(token);
    }

    /// <summary>
    /// Retrieves user's information from database, using JWT Token claims
    /// </summary>
    /// <returns>User model</returns>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserInfoDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Authorize]
    [Route("info")]
    [HttpGet]
    public async Task<IActionResult> Info()
    {
        // Reading UserId from JWT Claim
        var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        if (userIdClaim == null) return BadRequest("Wrong JWT token");
        // Retrieving User information from Database Context
        var userIdValue = Int32.Parse(userIdClaim.Value);
        var userInfo = await _context.Users!.FirstOrDefaultAsync(u => u.Id == userIdValue);
        if (userInfo == null) return NotFound("User not found");
        // Returning safe model
        return Ok(_mapper.Map<User,UserInfoDto>(userInfo));
    }
}