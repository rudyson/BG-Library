using BG.NET.Library.BusinessLogicLayer.Interfaces;
using BG.NET.Library.Models;
using BG.NET.Library.Models.Dto.Library;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BG.NET.Library.API.Data.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _service;
        private readonly IValidator<BookDtoNew> _validateNewBook;
        private readonly IValidator<BookDtoUpdate> _validateUpdateBook;

        public BookController(
            IBookService service,
            IValidator<BookDtoNew> validateNewBook,
            IValidator<BookDtoUpdate> validateUpdateBook)
        {
            _service = service;
            _validateNewBook = validateNewBook;
            _validateUpdateBook = validateUpdateBook;
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericPaginationModel<BookDtoFull>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllBooks(
            int page = 1,
            int size = 10
        )
        {
            var bookList = await _service.AllPaginatedFull(page,size);
            if (bookList == null) return BadRequest("Pagination model broken");
            return bookList.Entities.IsNullOrEmpty()
                ? NotFound()
                : Ok(bookList);
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BookDtoFull))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetBook(int id)
        {
            var book = await _service.FindFull(id);
            return book != null
                ? Ok(book)
                : NotFound();
        }

        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(BookDtoNew))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost]
        public async Task<IActionResult> CreateBook(BookDtoNew book)
        {
            var validationResult = await _validateNewBook.ValidateAsync(book);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.ToDictionary());
            }
            var bookCreated = await _service.Create(book);
            return bookCreated != null
                ? CreatedAtAction(nameof(CreateBook),bookCreated)
                : BadRequest("Book is not created");
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateBook(int id, BookDtoUpdate book)
        {
            var validationResult = await _validateUpdateBook.ValidateAsync(book);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.ToDictionary());
            }
            if (await _service.FindShort(id) == null)
                return NotFound();
            var bookUpdated = await _service.Update(id, book);
            return bookUpdated != null
                ? Ok()
                : BadRequest("No action needed");
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            if (await _service.FindShort(id) == null)
                return NotFound();
            return await _service.Delete(id) ? Ok() : BadRequest();
        }
    }
}