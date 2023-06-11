using GreenerGrain.Framework.Database.EfCore.Mapping;
using GreenerGrain.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace GreenerGrain.Data.Mapping
{
    public class AccountMap : BaseAuditEntityMap<Account, Guid>
    {
        protected override void CreateModel(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Account");

            builder
                 .Property(b => b.Login)
                 .HasColumnType("varchar(500)")
                 .IsRequired();

            builder
                 .Property(b => b.Name)
                 .HasColumnType("varchar(150)")
                 .IsRequired();
        }
    }
}
