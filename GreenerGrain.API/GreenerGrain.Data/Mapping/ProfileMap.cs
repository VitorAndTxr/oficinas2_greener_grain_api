﻿using GreenerGrain.Framework.Database.EfCore.Mapping;
using GreenerGrain.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Reflection;
using System.Security.Cryptography.Xml;

namespace GreenerGrain.Data.Mapping
{
    public class ProfileMap : BaseAuditEntityMap<Profile, Guid>
    {
        protected override void CreateModel(EntityTypeBuilder<Profile> builder)
        {
            builder.ToTable("Profile");

            builder
                 .Property(b => b.Name)
                 .HasColumnType("varchar(150)")
                 .IsRequired();

            builder
                 .Property(b => b.Code)
                 .HasColumnType("varchar(80)")
                 .IsRequired();

        }
    }
}
