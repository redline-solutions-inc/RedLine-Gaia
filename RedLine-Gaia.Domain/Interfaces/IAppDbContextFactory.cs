using Microsoft.EntityFrameworkCore;

namespace RedLine_Gaia.Domain.Interfaces;

public interface IAppDbContextFactory<T>
    where T : DbContext
{
    T dbContext();
}
