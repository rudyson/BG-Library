using System.Collections.Generic;
using System.Security.Claims;
using BGNet.TestAssignment.BusinessLogic.Interfaces.Auth;
using BGNet.TestAssignment.Common.WebApi.Models.Responses;
using BGNet.TestAssignment.Models.Dto.Auth;
using BGNet.TestAssignment.Models.Requests.Auth;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BGNet.TestAssignment.Api.Controllers.Auth;

[Authorize]
[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IIdentityService _service;
    private readonly IValidator<RegisterRequest> _validateRegistration;
    private readonly IValidator<LoginRequest> _validateLogin;

    public AuthController(
        IIdentityService service,
        IValidator<RegisterRequest> validateRegistration,
        IValidator<LoginRequest> validateLogin
        )
    {
        _service = service;
        _validateRegistration = validateRegistration;
        _validateLogin = validateLogin;
    }

    /// <summary>
    /// Provides user registration, passing RegisterRequest model
    /// </summary>
    /// <param name="user">RegisterRequest model, which contains required fields to register in system</param>
    /// <returns></returns>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [AllowAnonymous]
    [Route("register")]
    [HttpPost]
    public async Task<ResponseWrapper<UserInfoDto>> Register(RegisterRequest user)
    {
        var validationResult = await _validateRegistration.ValidateAsync(user);
        if (!validationResult.IsValid)
            return ResponseWrapper<UserInfoDto>.Wrap(validation: validationResult.ToDictionary());
        return ResponseWrapper<UserInfoDto>.Wrap(await _service.Register(user));
    }

    /// <summary>
    /// Generates token to authorize user in system
    /// </summary>
    /// <param name="user">Username and password</param>
    /// <returns>JWT Token with 1 hour life duration</returns>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TokenCreatedDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [AllowAnonymous]
    [Route("login")]
    [HttpPost]
    public async Task<ResponseWrapper<TokenCreatedDto>> Login(LoginRequest user)
    {
        var validationResult = await _validateLogin.ValidateAsync(user);
        if (!validationResult.IsValid)
            return ResponseWrapper<TokenCreatedDto>.Wrap(validation: validationResult.ToDictionary());
        var token = await _service.Login(user);
        return ResponseWrapper<TokenCreatedDto>.Wrap(token);
    }

    /// <summary>
    /// Retrieves user's information from database, using JWT Token claims
    /// </summary>
    /// <returns>User model</returns>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserInfoDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize]
    [Route("info")]
    [HttpGet]
    public async Task<ResponseWrapper<UserInfoDto>> Info()
    {
        var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        if (userIdClaim == null) return ResponseWrapper<UserInfoDto>.Wrap(ResponseCodes.WrongAuthorizationToken);
        var userInfo = await _service.Info(userIdClaim);

        return userInfo == null
            ? ResponseWrapper<UserInfoDto>.Wrap(ResponseCodes.NotFound)
            : ResponseWrapper<UserInfoDto>.Wrap(userInfo);
    }
}