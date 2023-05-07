using Microsoft.EntityFrameworkCore;
using System;

namespace GreenerGrain.Framework.Database.EfCore.Factory
{
    public interface IDbFactory : IDisposable
    {        
        public DbContext DbContext { get; }
    }
}
