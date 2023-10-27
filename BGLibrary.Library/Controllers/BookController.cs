using AutoMapper;
using BG.NET.Library.DataAccessLayer.Interfaces;
using BG.NET.Library.Models.Dto.Library;
using BG.NET.Library.Models.Entities.Library;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BGLibrary.Library.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;
        private readonly IBookRepository _repository;
        private readonly IMapper _mapper;

        public BookController(
            ILogger<BookController> logger,
            IBookRepository repository,
            IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            var bookList = await _repository.GetAll();
            return bookList.IsNullOrEmpty() ? NotFound() : Ok(bookList);
        }

        [AllowAnonymous]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetBook(int id)
        {
            var book = await _repository.GetSingle(id);
            return book == null ? NotFound() : Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook(NewBookDto newBook)
        {
            if (!ModelState.IsValid) return BadRequest();
            var bookConverted = _mapper.Map<NewBookDto, Book>(newBook);
            return await _repository.Create(bookConverted) ? Ok() : BadRequest();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateBook(int id, UpdateBookDto updateBook)
        {
            var bookConverted = _mapper.Map<UpdateBookDto, Book>(updateBook);
            bookConverted.Id = id;

            var author = await _repository.GetAuthor(id);
            if (author != null && updateBook.AuthorId != author.Id)
                bookConverted.Author = author;

            return await _repository.Update(bookConverted) ? Ok() : BadRequest();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            return await _repository.Delete(id) ? Ok() : BadRequest();
        }

        [AllowAnonymous]
        [HttpGet("{id:int}/author")]
        public async Task<IActionResult> GetAuthor(int id)
        {
            var author = await _repository.GetAuthor(id);
            return author == null ? NotFound() : Ok(author);
        }

        [HttpPost("{id:int}/author")]
        public async Task<IActionResult> SetAuthor(int id, int authorId)
        {
            return await _repository.SetAuthor(id, authorId) ? BadRequest() : Ok();
        }
    }
}