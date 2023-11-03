using BGNet.TestAssignment.BusinessLogic.Interfaces.Library;
using BGNet.TestAssignment.Common.WebApi.Models.Pagination;
using BGNet.TestAssignment.Common.WebApi.Models.Responses;
using BGNet.TestAssignment.Models.Dto.Library;
using BGNet.TestAssignment.Models.Requests.Library;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BGNet.TestAssignment.Api.Controllers.Library
{
    [Authorize]
    [Route("library/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _service;
        private readonly IValidator<BookCreateRequest> _validateNewBook;
        private readonly IValidator<BookUpdateRequest> _validateUpdateBook;

        public BookController(
            IBookService service,
            IValidator<BookCreateRequest> validateNewBook,
            IValidator<BookUpdateRequest> validateUpdateBook)
        {
            _service = service;
            _validateNewBook = validateNewBook;
            _validateUpdateBook = validateUpdateBook;
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericPaginationModel<BookFullInfoDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet]
        public async Task<ResponseWrapper<GenericPaginationModel<BookFullInfoDto>>> GetAllBooks(
            int page = 1,
            int size = 10
        )
        {
            var bookList = await _service.AllPaginatedFull(page, size);
            return (bookList == null)
                ? ResponseWrapper<GenericPaginationModel<BookFullInfoDto>>.Wrap(ResponseCodes.PaginationBroken)
                : ResponseWrapper<GenericPaginationModel<BookFullInfoDto>>.Wrap(bookList);
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BookFullInfoDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id:int}")]
        public async Task<ResponseWrapper<BookFullInfoDto>> GetBook(int id)
        {
            var book = await _service.FindFull(id);
            return book != null
                ? ResponseWrapper<BookFullInfoDto>.Wrap(book)
                : ResponseWrapper<BookFullInfoDto>.Wrap(ResponseCodes.NotFound);
        }

        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(BookShortInfoDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpPost]
        public async Task<ResponseWrapper<BookShortInfoDto>> CreateBook(BookCreateRequest book)
        {
            var validationResult = await _validateNewBook.ValidateAsync(book);
            if (!validationResult.IsValid) return ResponseWrapper<BookShortInfoDto>.Wrap(validationResult.ToDictionary());

            var bookCreated = await _service.Create(book);
            return bookCreated != null
                ? ResponseWrapper<BookShortInfoDto>.Wrap(bookCreated)
                : ResponseWrapper<BookShortInfoDto>.Wrap(ResponseCodes.CreateRequestFailed);
        }

        [ProducesResponseType(StatusCodes.Status202Accepted, Type = typeof(BookShortInfoDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpPut("{id:int}")]
        public async Task<ResponseWrapper<BookShortInfoDto>> UpdateBook(int id, BookUpdateRequest book)
        {
            var validationResult = await _validateUpdateBook.ValidateAsync(book);
            if (!validationResult.IsValid) return ResponseWrapper<BookShortInfoDto>.Wrap(validationResult.ToDictionary());
            if (!await _service.Exists(id)) return ResponseWrapper<BookShortInfoDto>.Wrap(ResponseCodes.NotFound);

            var bookUpdated = await _service.Update(id, book);
            return bookUpdated != null
                ? ResponseWrapper<BookShortInfoDto>.Wrap(bookUpdated)
                : ResponseWrapper<BookShortInfoDto>.Wrap(ResponseCodes.NothingToUpdate);
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BookFullInfoDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id:int}")]
        public async Task<ResponseWrapper<BookFullInfoDto>> DeleteBook(int id)
        {
            if (await _service.Exists(id) == false) return ResponseWrapper<BookFullInfoDto>.Wrap(ResponseCodes.NotFound);
            var deletedBook = await _service.Delete(id);
            return deletedBook == null
                ? ResponseWrapper<BookFullInfoDto>.Wrap(ResponseCodes.DeleteRequestFailed)
                : ResponseWrapper<BookFullInfoDto>.Wrap(deletedBook);
        }
    }
}