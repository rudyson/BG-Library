using BG.NET.Library.BusinessLogicLayer.Interfaces;
using BG.NET.Library.Models.Dto.Library;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BGLibrary.Library.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class AuthorController : ControllerBase
{
    private readonly IAuthorService _service;

    public AuthorController(
        IAuthorService service
    )
    {
        _service = service;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetAllAuthors()
    {
        var authors = await _service.AllFull();
        return authors.IsNullOrEmpty()
            ? NotFound()
            : Ok(authors);
    }

    [AllowAnonymous]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetAuthor(int id)
    {
        var author = await _service.FindFull(id);
        return author != null
            ? Ok(author)
            : NotFound();
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateAuthor(AuthorDtoBase author)
    {
        var authorCreated = await _service.Create(author);
        return authorCreated != null
            ? CreatedAtAction(nameof(CreateAuthor),authorCreated)
            : BadRequest("Author is not created");
    }
    
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateAuthor(int id, AuthorDtoUpdate author)
    {
        if (await _service.FindShort(id) == null)
            return NotFound();
        var authorUpdated = await _service.Update(id, author);
        return authorUpdated != null
            ? Ok()
            : BadRequest("No action needed");
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAuthor(int id)
    {
        if (await _service.FindShort(id) == null)
            return NotFound();
        return await _service.Delete(id) ? Ok() : BadRequest();
    }
}