using BG.NET.Library.BusinessLogicLayer.Interfaces;
using BG.NET.Library.Models;
using BG.NET.Library.Models.Dto.Library;
using BG.NET.Library.Models.Entities.Library;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BG.NET.Library.API.Data.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class AuthorController : ControllerBase
{
    private readonly IAuthorService _service;
    private readonly IValidator<AuthorDtoBase> _validateNewAuthor;
    private readonly IValidator<AuthorDtoUpdate> _validateUpdateAuthor;

    public AuthorController(
        IAuthorService service,
        IValidator<AuthorDtoBase> validateNewAuthor,
        IValidator<AuthorDtoUpdate> validateUpdateAuthor
    )
    {
        _service = service;
        _validateNewAuthor = validateNewAuthor;
        _validateUpdateAuthor = validateUpdateAuthor;
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericPaginationModel<AuthorDtoFull>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetAllAuthors(
        int page = 1,
        int size = 10
        )
    {
        var authors = await _service.AllPaginatedFull(page, size);
        if (authors == null) return BadRequest("Pagination model broken");
        return authors.Entities.IsNullOrEmpty()
            ? NotFound()
            : Ok(authors);
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthorDtoFull))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [AllowAnonymous]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetAuthor(int id)
    {
        var author = await _service.FindFull(id);
        return author != null
            ? Ok(author)
            : NotFound();
    }

    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AuthorDtoNoBooks))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpPost]
    public async Task<IActionResult> CreateAuthor(AuthorDtoBase author)
    {
        var validationResult = await _validateNewAuthor.ValidateAsync(author);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.ToDictionary());
        }
        var authorCreated = await _service.Create(author);
        return authorCreated != null
            ? CreatedAtAction(nameof(CreateAuthor), authorCreated)
            : BadRequest("Author is not created");
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateAuthor(int id, AuthorDtoUpdate author)
    {
        var validationResult = await _validateUpdateAuthor.ValidateAsync(author);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.ToDictionary());
        }
        if (await _service.FindShort(id) == null)
            return NotFound();
        var authorUpdated = await _service.Update(id, author);
        return authorUpdated != null
            ? Ok()
            : BadRequest("No action needed");
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAuthor(int id)
    {
        if (await _service.FindShort(id) == null)
            return NotFound();
        return await _service.Delete(id) ? Ok() : BadRequest();
    }
}