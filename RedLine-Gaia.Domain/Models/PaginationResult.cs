using System.Diagnostics.CodeAnalysis;

namespace RedLine_Gaia.Domain.Models;

[ExcludeFromCodeCoverage]
/// <summary>
/// Represents the result of a paginated query operation.
/// </summary>
/// <typeparam name="T">The type of entities in the result collection.</typeparam>
public class PaginationResult<T>
{
    /// <summary>
    /// The current page number.
    /// </summary>
    public int Page { get; set; }

    /// <summary>
    /// The number of items per page.
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// The total number of pages available.
    /// </summary>
    public int TotalPages { get; set; }

    /// <summary>
    /// The total number of records that match the query criteria.
    /// </summary>
    public int TotalSize { get; set; }

    /// <summary>
    /// The collection of results for the current page.
    /// </summary>
    public IEnumerable<T> Results { get; set; } = Enumerable.Empty<T>();

    /// <summary>
    /// Indicates whether there is a previous page available.
    /// </summary>
    public bool HasPreviousPage => Page > 1;

    /// <summary>
    /// Indicates whether there is a next page available.
    /// </summary>
    public bool HasNextPage => Page < TotalPages;

    /// <summary>
    /// Creates a new instance of PaginationResult.
    /// </summary>
    /// <param name="results">The results for the current page.</param>
    /// <param name="totalCount">The total number of records matching the query.</param>
    /// <param name="pageNumber">The current page number.</param>
    /// <param name="pageSize">The number of items per page.</param>
    public PaginationResult(IEnumerable<T> results, int totalCount, int pageNumber, int pageSize)
    {
        Results = results;
        TotalSize = totalCount;
        Page = pageNumber;
        PageSize = pageSize;
        TotalPages = (int)Math.Ceiling((double)totalCount / pageSize);
    }

    /// <summary>
    /// Creates an empty pagination result.
    /// </summary>
    public PaginationResult() { }
}
