namespace BGNet.TestAssignment.Common.WebApi.Models.Pagination;

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
    public int LastPage => Pages;
    public bool OnFirstPage => Page == FirstPage;
    public bool OnLastPage => Page == LastPage;
    public bool HasNextPage => NextPage != null && !OnLastPage;
    public bool HasPreviousPage => PreviousPage != null && !OnFirstPage;
    public required IEnumerable<T> Entities { get; init; }
}