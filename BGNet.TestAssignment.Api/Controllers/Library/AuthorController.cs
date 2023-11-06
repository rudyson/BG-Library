using BGNet.TestAssignment.BusinessLogic.Interfaces.Library;
using BGNet.TestAssignment.Common.WebApi.Models.Pagination;
using BGNet.TestAssignment.Common.WebApi.Models.Responses;
using BGNet.TestAssignment.Models.Dto.Library;
using BGNet.TestAssignment.Models.Requests.Library;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BGNet.TestAssignment.Api.Controllers.Library;

[Authorize]
[Route("library/[controller]")]
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
    [HttpGet]
    public async Task<ResponseWrapper<GenericPaginationModel<AuthorFullInfoDto>>> GetAllAuthors(
        int skip = 0, int take = 10,
        CancellationToken cancellationToken = default
        )
    {
        var authors = await _service.AllPaginatedSkipTakeFullAsync(skip, take, cancellationToken: cancellationToken);
        return (authors == null)
            ? ResponseWrapper<GenericPaginationModel<AuthorFullInfoDto>>.Wrap(ResponseCodes.NotFound)
            : ResponseWrapper<GenericPaginationModel<AuthorFullInfoDto>>.Wrap(authors);
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthorFullInfoDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{id:int}")]
    public async Task<ResponseWrapper<AuthorFullInfoDto>> GetAuthor(int id, CancellationToken cancellationToken = default)
    {
        var author = await _service.FindFullAsync(id, cancellationToken: cancellationToken);
        return author != null
            ? ResponseWrapper<AuthorFullInfoDto>.Wrap(author)
            : ResponseWrapper<AuthorFullInfoDto>.Wrap(ResponseCodes.NotFound);
    }

    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AuthorShortInfoDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [HttpPost]
    public async Task<ResponseWrapper<AuthorShortInfoDto>> CreateAuthor(AuthorCreateRequest author, CancellationToken cancellationToken = default)
    {
        var validationResult = await _validateNewAuthor.ValidateAsync(author, cancellation: cancellationToken);
        if (!validationResult.IsValid) return ResponseWrapper<AuthorShortInfoDto>.Wrap(validationResult.ToDictionary());

        var authorCreated = await _service.CreateAsync(author, cancellationToken: cancellationToken);
        return authorCreated != null
            ? ResponseWrapper<AuthorShortInfoDto>.Wrap(authorCreated)
            : ResponseWrapper<AuthorShortInfoDto>.Wrap(ResponseCodes.CreateRequestFailed);
    }

    [ProducesResponseType(StatusCodes.Status202Accepted, Type = typeof(AuthorShortInfoDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [HttpPut("{id:int}")]
    public async Task<ResponseWrapper<AuthorShortInfoDto>> UpdateAuthor(int id, AuthorUpdateRequest author, CancellationToken cancellationToken = default)
    {
        var validationResult = await _validateUpdateAuthor.ValidateAsync(author, cancellation: cancellationToken);
        if (!validationResult.IsValid) return ResponseWrapper<AuthorShortInfoDto>.Wrap(validationResult.ToDictionary());

        if (await _service.FindShortAsync(id, cancellationToken: cancellationToken) == null) return ResponseWrapper<AuthorShortInfoDto>.Wrap(ResponseCodes.NotFound);
        var authorUpdated = await _service.UpdateAsync(id, author, cancellationToken: cancellationToken);
        return authorUpdated != null
            ? ResponseWrapper<AuthorShortInfoDto>.Wrap(authorUpdated)
            : ResponseWrapper<AuthorShortInfoDto>.Wrap(ResponseCodes.NothingToUpdate);
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthorShortInfoDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpDelete("{id:int}")]
    public async Task<ResponseWrapper<AuthorShortInfoDto>> DeleteAuthor(int id, CancellationToken cancellationToken = default)
    {
        if (await _service.FindFullAsync(id, cancellationToken: cancellationToken) == null) return ResponseWrapper<AuthorShortInfoDto>.Wrap(ResponseCodes.NotFound);
        var deletedAuthor = await _service.DeleteAsync(id, cancellationToken: cancellationToken);
        return deletedAuthor == null
            ? ResponseWrapper<AuthorShortInfoDto>.Wrap(ResponseCodes.DeleteRequestFailed)
            : ResponseWrapper<AuthorShortInfoDto>.Wrap(deletedAuthor);
    }
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<AuthorAutocompleteDto>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("search")]
    public ResponseWrapper<List<AuthorAutocompleteDto>> SearchAuthors(string query, CancellationToken cancellationToken = default)
    {
        var authors = _service.SearchAsync(query, cancellationToken: cancellationToken);
        return (authors == null)
            ? ResponseWrapper<List<AuthorAutocompleteDto>>.Wrap(ResponseCodes.EmptyQuery)
            : ResponseWrapper<List<AuthorAutocompleteDto>>.Wrap(authors.ToList());
    }
}