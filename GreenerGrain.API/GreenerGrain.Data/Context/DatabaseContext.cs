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

        public DbSet<ServiceDeskType> ServiceDeskType { get; set; }
        public DbSet<ServiceDesk> ServiceDesk { get; set; }
        public DbSet<ServiceDeskOpeningHours> ServiceDeskOpeningHours { get; set; }

        public DbSet<ServiceDeskOfficer> ServiceDeskOfficer { get; set; }
        public DbSet<OfficerPause> OfficerPause { get; set; }
        public DbSet<OfficerHour> OfficerHour { get; set; }
        public DbSet<InstitutionServiceLocation> InstitutionServiceLocation { get; set; }
        public DbSet<Appointment> Appointment { get; set; }
        public DbSet<AppointmentQueue> AppointmentQueue { get; set; }
        public DbSet<Provider> Provider { get; set; }
        public DbSet<Property> Property { get; set; }
        public DbSet<InstitutionProvider> InstitutionProvider { get; set; }
        public DbSet<InstitutionProviderProperty> InstitutionProviderProperty { get; set; }
        
        public DbSet<GreenerGrain.Domain.Entities.Account> Account { get; set; }
        public DbSet<AccountProfile> AccountProfile { get; set; }
        public DbSet<Institution> Institution { get; set; }
        public DbSet<GreenerGrain.Domain.Entities.Profile> Profile { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
               .Entity<InstitutionProviderSettings>(eb =>
               {
                   eb.HasNoKey();
                   eb.ToView("institution_provider_settings");
               });

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