using BG.NET.Library.BusinessLogicLayer.Interfaces;
using BG.NET.Library.Models.Dto.Library;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BGLibrary.Library.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _service;

        public BookController(
            IBookService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            var bookList = await _service.AllFull();
            return bookList!=null
                ? Ok(bookList)
                : NotFound();
        }

        [AllowAnonymous]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetBook(int id)
        {
            var book = await _service.FindFull(id);
            return book != null
                ? Ok(book)
                : NotFound();
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateBook(BookDtoNew book)
        {
            var bookCreated = await _service.Create(book);
            return bookCreated != null
                ? CreatedAtAction(nameof(CreateBook),bookCreated)
                : BadRequest("Book is not created");
        }
        
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateBook(int id, BookDtoUpdate book)
        {
            if (await _service.FindShort(id) == null)
                return NotFound();
            var bookUpdated = await _service.Update(id, book);
            return bookUpdated != null
                ? Ok()
                : BadRequest("No action needed");
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            if (await _service.FindShort(id) == null)
                return NotFound();
            return await _service.Delete(id) ? Ok() : BadRequest();
        }
    }
}