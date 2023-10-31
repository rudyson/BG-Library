using System.Security.Claims;
using BG.NET.Library.Models.Dto.Auth;

namespace BG.NET.Library.BusinessLogicLayer.Interfaces;

public interface IIdentityService
{
    /// <summary>
    /// Provides user registration
    /// </summary>
    /// <param name="user">RegisterDto contains user's credentials</param>
    /// <returns>User id if it registered successfully</returns>
    public Task<int?> Register(RegisterDto user);
    /// <summary>
    /// Provides user authorization
    /// </summary>
    /// <param name="user">LoginDto contains username and password</param>
    /// <returns>JWT Token if user exists</returns>
    public Task<JwtTokenDto?> Login(LoginDto user);
    /// <summary>
    /// Retrieves user's information using context and signed JWT token
    /// </summary>
    /// <param name="claim">Security token claims</param>
    /// <returns>UserInfoDto with user's creadentials</returns>
    public Task<UserInfoDto?> Info(Claim claim);
}