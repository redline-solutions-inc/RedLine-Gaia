namespace RedLine_Gaia.Domain.Interfaces;

public interface IRepository<T>
{
    Task<T?> GetById(int id);
    void Add(T entity);
    Task AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
}
