using System.Security.Claims;
using BGNet.TestAssignment.Models.Dto.Auth;
using BGNet.TestAssignment.Models.Requests.Auth;

namespace BGNet.TestAssignment.BusinessLogic.Interfaces.Auth;

public interface IIdentityService
{
    /// <summary>
    /// Provides user registration
    /// </summary>
    /// <param name="user">RegisterRequest contains user's credentials</param>
    /// <returns>User id if it registered successfully</returns>
    public Task<int?> Register(RegisterRequest user);
    /// <summary>
    /// Provides user authorization
    /// </summary>
    /// <param name="user">LoginRequest contains username and password</param>
    /// <returns>JWT Token if user exists</returns>
    public Task<TokenCreatedDto?> Login(LoginRequest user);
    /// <summary>
    /// Retrieves user's information using context and signed JWT token
    /// </summary>
    /// <param name="claim">Security token claims</param>
    /// <returns>UserInfoDto with user's creadentials</returns>
    public Task<UserInfoDto?> Info(Claim claim);
}