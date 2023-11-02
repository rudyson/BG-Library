
using System.Security.Claims;
using BG.NET.Library.BusinessLogic.Interfaces;
using BG.NET.Library.Models.Dto;
using BG.NET.Library.Models.Requests;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BG.NET.Library.API.Identity.Controllers;

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
    public async Task<IActionResult> Register(RegisterRequest user)
    {
        var validationResult = await _validateRegistration.ValidateAsync(user);
        if (!validationResult.IsValid) return UnprocessableEntity(validationResult.ToDictionary());
        var createdUserId = await _service.Register(user);
        return createdUserId == null
            ? BadRequest("Unable to register user")
            : Ok($"User registered, Id: {createdUserId}");
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
    public async Task<IActionResult> Login(LoginRequest user)
    {
        var validationResult = await _validateLogin.ValidateAsync(user);
        if (!validationResult.IsValid) return UnprocessableEntity(validationResult.ToDictionary());
        var token = await _service.Login(user);
        return token == null
            ? NotFound("User is not exists or provided credentials wrong")
            : Ok(token);
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
    [HttpGet("Information about user")]
    public async Task<IActionResult> Info()
    {
        var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        if (userIdClaim == null) return BadRequest("Wrong JWT token");
        var userInfo = await _service.Info(userIdClaim);
        return userInfo == null
            ? NotFound("User not found")
            : Ok(userInfo);
    }
}