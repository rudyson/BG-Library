using System.ComponentModel.DataAnnotations;

namespace BGLibrary.Library.Models.Dto;

public class UpdateBookDto : NewBookDto
{
    public int? AuthorId { get; set; }
}