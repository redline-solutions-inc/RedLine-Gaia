namespace RedLine_Gaia.Domain.Interfaces;

/// <summary>
/// The Unit of Work groups one or more operations into a
/// single transaction and executes them by applying the
/// principle of do everything or do nothing.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Calls the current scoped DbContext SaveChanges method to commit
    /// any and all transactions, using EF Core change detection, to the
    /// database.
    /// </summary>
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
