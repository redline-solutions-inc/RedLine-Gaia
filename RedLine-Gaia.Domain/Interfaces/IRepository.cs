namespace RedLine_Gaia.Domain.Interfaces;

public interface IRepository<T>
{
    Task<T?> GetById(int id);

    /// <inheritdoc cref="Microsoft.EntityFrameworkCore.DbContext.Add(object)" />
    void Add(T entity);

    /// <inheritdoc cref="Microsoft.EntityFrameworkCore.DbContext.AddAsync(object, CancellationToken)" />
    Task AddAsync(T entity);

    /// <inheritdoc cref="Microsoft.EntityFrameworkCore.DbContext.Update(object)" />
    void Update(T entity);

    /// <inheritdoc cref="Microsoft.EntityFrameworkCore.DbContext.Remove(object)" />
    void Delete(T entity);
}
