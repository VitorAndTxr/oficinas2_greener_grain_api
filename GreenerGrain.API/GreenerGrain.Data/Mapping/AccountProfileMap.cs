using GreenerGrain.Framework.Database.EfCore.Mapping;
using GreenerGrain.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace GreenerGrain.Data.Mapping
{
    public class AccountProfileMap : BaseAuditEntityMap<AccountProfile, Guid>
    {
        protected override void CreateModel(EntityTypeBuilder<AccountProfile> builder)
        {
            builder.ToTable("AccountProfile");

          
            builder
                 .Property(b => b.AccountId)
                 .IsRequired();

            builder
                 .Property(b => b.ProfileId)
                 .IsRequired();

            builder
                .Property(b => b.CreationDate)
                .HasColumnType("datetime2")
                .IsRequired();

            builder
                .Property(b => b.UpdateDate)
                .HasColumnType("datetime2");

            builder
                .Property(b => b.DeleteDate)
                .HasColumnType("datetime2");

            builder.HasOne(x => x.Account);
            builder.HasOne(x => x.Profile);            
        }
    }
}
