using AutoMapper;
using BG.NET.Library.DataAccessLayer.Interfaces;
using BG.NET.Library.Models.Dto.Library;
using BG.NET.Library.Models.Entities.Library;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BGLibrary.Library.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class AuthorController : ControllerBase
{
    private readonly ILogger<AuthorController> _logger;
    private readonly IAuthorRepository _repository;
    private readonly IMapper _mapper;

    public AuthorController(
        IAuthorRepository repository,
        ILogger<AuthorController> logger,
        IMapper mapper
    )
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetAllAuthors()
    {
        var authors = await _repository.GetAll();
        return authors.IsNullOrEmpty() ? NotFound() : Ok(authors);
    }

    [AllowAnonymous]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetAuthor(int id)
    {
        var author = await _repository.GetSingle(id);
        return author == null ? NotFound() : Ok(author);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAuthor(NewAuthorDto author)
    {
        if (!ModelState.IsValid) return BadRequest();
        var bookConverted = _mapper.Map<NewAuthorDto, Author>(author);
        return await _repository.Create(bookConverted) ? Ok() : BadRequest();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateAuthor(int id, NewAuthorDto author)
    {
        var authorConverted = _mapper.Map<NewAuthorDto, Author>(author);
        authorConverted.Id = id;
        return await _repository.Update(authorConverted) ? Ok() : BadRequest();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAuthor(int id)
    {
        return await _repository.Delete(id) ? Ok() : BadRequest();
    }

    [AllowAnonymous]
    [HttpGet("{id:int}/books")]
    public async Task<IActionResult> GetBooks(int id)
    {
        var books = await _repository.GetBooks(id);
        return books.IsNullOrEmpty() ? NotFound() : Ok(books);
    }

    [HttpPost("{id:int}/books")]
    public async Task<IActionResult> AddBook(int id, int bookId)
    {
        return await _repository.AddBook(id, bookId) ? Ok() : BadRequest();
    }
}