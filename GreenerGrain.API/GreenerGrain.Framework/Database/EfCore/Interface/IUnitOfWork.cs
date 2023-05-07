using System.Threading.Tasks;

namespace GreenerGrain.Framework.Database.EfCore.Interface
{
    public interface IUnitOfWork
    {
        ///// <summary>
        ///// Persists multiple repositories which is sharing a single database context. <br />
        ///// When a unit of work is complete we invoke the SaveChangesAsync from context.
        ///// </summary>
        Task<bool> CommitAsync();
    }
}
