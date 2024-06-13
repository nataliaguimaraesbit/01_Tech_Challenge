﻿using LocalFriendzApi.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LocalFriendzApi.Infrastructure.Data.Mappings
{
    public class AreaCodeMapping : IEntityTypeConfiguration<AreaCode>
    {
        public void Configure(EntityTypeBuilder<AreaCode> builder)
        {
            builder.ToTable("TB_AREA_CODE");
            builder.HasKey(c => c.IdAreaCode).HasName("id_area_code");

            builder.Property(c => c.CodeRegion)
                .IsRequired(true)
                .HasColumnName("code_region")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(2);
        }
    }
}
