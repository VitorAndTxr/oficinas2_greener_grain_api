using GreenerGrain.Framework.Database.EfCore.Context;
using GreenerGrain.Framework.Database.EfCore.Model;
using GreenerGrain.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Module = GreenerGrain.Domain.Entities.Module;

namespace GreenerGrain.Data.Context
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public DbContext GetDbContext()
        {
            return this;
        }
        
        public DbSet<Account> Account { get; set; }
        public DbSet<AccountProfile> AccountProfile { get; set; }
        public DbSet<Profile> Profile { get; set; }
        public DbSet<AccountWallet> AccountWallet { get; set; }
        public DbSet<Unit> Unit { get; set; }
        public DbSet<Module> Module { get; set; }
        public DbSet<Grain> Grain { get; set; }
        public DbSet<BuyTransaction> BuyTransaction { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {            
            try
            {
                foreach (var entity in ChangeTracker
                    .Entries()
                    .Where(x => x.Entity is AuditEntity<Guid> && x.State == EntityState.Modified)
                    .Select(x => x.Entity)
                    .Cast<AuditEntity<Guid>>())
                {
                    entity.UpdateDate = DateTime.Now;
                }

                return base.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException updateConcurrencyException)
            {
                Debug.Print("DbUpdateConcurrencyException");
                Debug.Print(updateConcurrencyException.InnerException.Message);
                throw updateConcurrencyException;
            }
            catch (DbUpdateException updateException)
            {
                Debug.Print("DbUpdateException");
                Debug.Print(updateException.InnerException.Message);
                throw updateException;
            }
        }

    }
}