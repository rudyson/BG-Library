namespace BG.NET.Library.Models;

public class GenericPaginationModel<T> where T : class
{
    public int Page { get; init; } // Current page number
    public int PageSize { get; init; } // Current page items counted
    public int TotalSize { get; init; } // All items counted
    public int Pages { get; init; } // Total pages
    public int NumberSkipped { get; init; } // Skipped elements
    public int? NextPage => Pages == Page ? null : Page + 1;
    public int? PreviousPage => Pages == Page ? null : Page - 1;
    public int FirstPage => 1;
    public int LastPage => this.Pages;
    public bool OnFirstPage => this.Page == this.FirstPage;
    public bool OnLastPage => this.Page == this.LastPage;
    public bool HasNextPage => this.NextPage != null;
    public bool HasPreviousPage => this.PreviousPage != null;
    public required IEnumerable<T> Entities { get; init; }
}