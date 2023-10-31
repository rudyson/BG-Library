
using System.Security.Claims;
using BG.NET.Library.BusinessLogicLayer.Interfaces;
using BG.NET.Library.Models.Dto.Auth;
using BG.NET.Library.Models.Entities.Auth;
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
    private readonly IValidator<RegisterDto> _validateRegistration;
    private readonly IValidator<LoginDto> _validateLogin;

    public AuthController(
        IIdentityService service,
        IValidator<RegisterDto> validateRegistration,
        IValidator<LoginDto> validateLogin
        )
    {
        _service = service;
        _validateRegistration = validateRegistration;
        _validateLogin = validateLogin;
    }

    /// <summary>
    /// Provides user registration, passing RegisterDto model
    /// </summary>
    /// <param name="user">RegisterDto model, which contains required fields to register in system</param>
    /// <returns></returns>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [AllowAnonymous]
    [Route("register")]
    [HttpPost]
    public async Task<IActionResult> Register(RegisterDto user)
    {
        var validationResult = await _validateRegistration.ValidateAsync(user);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.ToDictionary());
            //return Results.ValidationProblem(validationResult.ToDictionary());
        }
        var createdUserId = await _service.Register(user);
        return createdUserId == null
            ? BadRequest("User already exists")
            : Ok($"User registered, Id: {createdUserId}");
    }

    /// <summary>
    /// Generates token to authorize user in system
    /// </summary>
    /// <param name="user">Username and password</param>
    /// <returns>JWT Token with 1 hour life duration</returns>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(JwtTokenDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [AllowAnonymous]
    [Route("login")]
    [HttpPost]
    public async Task<IActionResult> Login(LoginDto user)
    {
        var validationResult = await _validateLogin.ValidateAsync(user);
        if (!validationResult.IsValid)
        {
            //return (IActionResult)Results.ValidationProblem(validationResult.ToDictionary());
            return BadRequest(validationResult.ToDictionary());
        }
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
    [HttpGet]
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