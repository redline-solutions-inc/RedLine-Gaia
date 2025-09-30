using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace RedLine_Gaia.Domain.Models;

[ExcludeFromCodeCoverage]
/// <summary>
/// Base class for pagination models containing common pagination properties.
/// </summary>
public abstract class BasePaginationModel
{
    /// <summary>
    /// The field to sort by. If null or empty, uses default sorting.
    /// </summary>
    public string? SortBy { get; set; }

    /// <summary>
    /// The sort direction. Defaults to ascending.
    /// </summary>
    public SortDirection SortDirection { get; set; } = SortDirection.Ascending;

    /// <summary>
    /// The current page number. Must be greater than 0.
    /// </summary>
    [Range(1, int.MaxValue, ErrorMessage = "Page number must be greater than 0")]
    public int PageNumber { get; set; } = 1;

    /// <summary>
    /// The number of items per page. Must be between 1 and 100.
    /// </summary>
    [Range(1, 100, ErrorMessage = "Page size must be between 1 and 100")]
    public int PageSize { get; set; } = 10;

    /// <summary>
    /// Calculates the number of items to skip for pagination.
    /// </summary>
    public int Skip => (PageNumber - 1) * PageSize;
}

/// <summary>
/// Represents the sort direction for pagination.
/// </summary>
public enum SortDirection
{
    Ascending,
    Descending,
}
