using BG.NET.Library.BusinessLogic.Interfaces;
using BG.NET.Library.Models.Dto;
using BG.NET.Library.Models.Generic;
using BG.NET.Library.Models.Requests;
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
    private readonly IValidator<AuthorCreateRequest> _validateNewAuthor;
    private readonly IValidator<AuthorUpdateRequest> _validateUpdateAuthor;

    public AuthorController(
        IAuthorService service,
        IValidator<AuthorCreateRequest> validateNewAuthor,
        IValidator<AuthorUpdateRequest> validateUpdateAuthor
    )
    {
        _service = service;
        _validateNewAuthor = validateNewAuthor;
        _validateUpdateAuthor = validateUpdateAuthor;
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericPaginationModel<AuthorFullInfoDto>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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
            : Ok();
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthorFullInfoDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetAuthor(int id)
    {
        var author = await _service.FindFull(id);
        return author != null
            ? Ok(author)
            : NotFound();
    }

    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AuthorShortInfoDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [HttpPost]
    public async Task<IActionResult> CreateAuthor(AuthorCreateRequest author)
    {
        var validationResult = await _validateNewAuthor.ValidateAsync(author);
        if (!validationResult.IsValid) return UnprocessableEntity(validationResult.ToDictionary());

        var authorCreated = await _service.Create(author);
        return authorCreated != null
            ? CreatedAtAction(nameof(CreateAuthor), authorCreated)
            : BadRequest("Author is not created");
    }

    [ProducesResponseType(StatusCodes.Status202Accepted, Type = typeof(AuthorShortInfoDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateAuthor(int id, AuthorUpdateRequest author)
    {
        var validationResult = await _validateUpdateAuthor.ValidateAsync(author);
        if (!validationResult.IsValid) return UnprocessableEntity(validationResult.ToDictionary());

        if (await _service.FindShort(id) == null) return NotFound();
        var authorUpdated = await _service.Update(id, author);
        return authorUpdated != null
            ? AcceptedAtAction(nameof(UpdateAuthor), authorUpdated)
            : BadRequest("No action needed");
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAuthor(int id)
    {
        if (await _service.FindFull(id) == null) return NotFound();
        return await _service.Delete(id) ? Ok() : BadRequest();
    }
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<AuthorAutocompleteDto>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("search")]
    public IActionResult SearchAuthors(string query)
    {
        var authors = _service.Search(query);
        if (authors == null) return BadRequest("Empty query string");
        return authors.Any() ? Ok(authors.ToList()) : NotFound("No authors found");
    }
}