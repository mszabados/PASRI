﻿using HRC.DB.Master.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRC.DB.Master.Persistence.EntityConfigurations
{
    /// <summary>
    /// Configures the database schema for the domain model
    /// <see cref="ReferenceRank"/> for use with code-first migrations
    /// </summary>
    /// <remarks>
    /// Data annotations should not be added to <see cref="ReferenceRank"/> so the code
    /// controlling database schema remains maintained only in this class.
    /// </remarks>
    public class ReferenceRankConfiguration : IEntityTypeConfiguration<ReferenceRank>
    {
        public const int CodeLength = 3;
        public const int LongNameLength = 30;

        public void Configure(EntityTypeBuilder<ReferenceRank> builder)
        {
            builder.ToTable("RE_RANK", "PERSON");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnName("re_rank_id")
                .IsRequired();

            builder.Property(p => p.Code)
                .HasColumnName("code")
                .HasColumnType($"varchar({CodeLength})")
                .IsRequired();
            builder.HasIndex(c => c.Code).IsUnique();

            builder.Property(p => p.LongName)
                .HasColumnName("long_name")
                .HasColumnType($"varchar({LongNameLength})")
                .IsRequired();

            builder.Property(p => p.CreatedDate)
                .HasColumnName("created_date")
                .HasColumnType("datetime");

            builder.Property(p => p.CreatedBy)
                .HasColumnName("created_by")
                .HasColumnType("varchar(30)");

            builder.Property(p => p.ModifiedDate)
                .HasColumnName("last_modified_date")
                .HasColumnType("datetime");

            builder.Property(p => p.ModifiedBy)
                .HasColumnName("last_modified_by")
                .HasColumnType("varchar(30)");
        }
    }
}
