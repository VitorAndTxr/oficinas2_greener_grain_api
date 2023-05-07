using Microsoft.EntityFrameworkCore;

namespace GreenerGrain.Framework.Database.EfCore.Context
{
    public interface IDatabaseContext
    {
        DbContext GetDbContext();
    }  
}
