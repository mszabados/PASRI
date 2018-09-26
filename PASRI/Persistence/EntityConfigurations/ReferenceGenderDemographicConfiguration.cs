﻿using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PASRI.API.Core.Domain;

namespace PASRI.API.Persistence.EntityConfigurations
{
    /// <summary>
    /// Configures the database schema for the domain model
    /// <see cref="ReferenceGenderDemographic"/> for use with code-first migrations
    /// </summary>
    /// <remarks>
    /// Data annotations should not be added to <see cref="ReferenceGenderDemographic"/> so the code
    /// controlling database schema remains maintained only in this class.
    /// </remarks>
    public class ReferenceGenderDemographicConfiguration : IEntityTypeConfiguration<ReferenceGenderDemographic>
    {
        public void Configure(EntityTypeBuilder<ReferenceGenderDemographic> builder)
        {
            builder.ToTable("RE_GENDER", schema: "PERSON");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnName("gender_id")
                .IsRequired();

            builder.Property(p => p.Code)
                .HasColumnName("code")
                .HasColumnType("char(1)")
                .IsRequired();
            builder.HasIndex(c => c.Code).IsUnique();

            builder.Property(p => p.Description)
                .HasColumnName("description")
                .HasColumnType("varchar(7)")
                .IsRequired();

            builder.Property(p => p.CreatedDate)
                .HasColumnName("created_date")
                .HasColumnType("datetime");

            builder.Property(p => p.CreatedBy)
                .HasColumnName("created_by")
                .HasColumnType("varchar(30)");

            builder.Property(p => p.ModifiedDate)
                .HasColumnName("modified_date")
                .HasColumnType("datetime");

            builder.Property(p => p.ModifiedBy)
                .HasColumnName("modified_by")
                .HasColumnType("varchar(30)");
        }
    }
}
