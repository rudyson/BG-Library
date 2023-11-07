namespace BGNet.TestAssignment.Common.WebApi.Models.Pagination;
/// <summary>
/// Helps to calculate pagination
/// </summary>
public static class PaginationCalculationAssistant
{
    /// <summary>
    /// Skipped items of sequence
    /// </summary>
    /// <param name="page">Current page</param>
    /// <param name="size">Size of page</param>
    /// <returns>Skipped items count</returns>
    public static int Skipped(int page, int size)
    {
        return (page - 1) * size;
    }
    /// <summary>
    /// Calculates number of available pages
    /// </summary>
    /// <param name="total">Count of all items in sequence</param>
    /// <param name="size">Size of page</param>
    /// <returns>Pages amount</returns>
    public static int TotalPages(int total, int size)
    {
        return (total - 1) / size + 1;
    }
    /// <summary>
    /// Calculates current page number for skip-take pagination
    /// </summary>
    /// <param name="skip">Amount of skipped items</param>
    /// <param name="take">Amount of taken items</param>
    /// <returns></returns>
    public static int CurrentPageSkipTake(int skip, int take)
    {
        return (skip > 0) ? (skip / take) + 1 : 1;
    }
}