using GreenerGrain.Framework.Database.EfCore.Mapping;
using GreenerGrain.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace GreenerGrain.Data.Mapping
{
    public class AccountWalletMap : BaseAuditEntityMap<AccountWallet, Guid>
    {
        protected override void CreateModel(EntityTypeBuilder<AccountWallet> builder)
        {
            builder.ToTable("AccountWallet");

            builder
                 .Property(b => b.Credits)
                 .IsRequired();

            builder
                 .Property(b => b.AccountId)
                 .IsRequired();

            builder.HasOne(x => x.Account)
                .WithOne(x => x.AccountWallet);

        }
    }
}
