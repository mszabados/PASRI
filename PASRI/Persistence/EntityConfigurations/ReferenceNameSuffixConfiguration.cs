using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PASRI.API.Core.Domain;

namespace PASRI.API.Persistence.EntityConfigurations
{
    /// <summary>
    /// Configures the database schema for the domain model
    /// <see cref="ReferenceNameSuffix"/> for use with code-first migrations
    /// </summary>
    /// <remarks>
    /// Data annotations should not be added to <see cref="ReferenceNameSuffix"/> so the code
    /// controlling database schema remains maintained only in this class.
    /// </remarks>
    public class ReferenceNameSuffixConfiguration : IEntityTypeConfiguration<ReferenceNameSuffix>
    {
        public void Configure(EntityTypeBuilder<ReferenceNameSuffix> builder)
        {
            builder.ToTable("RE_SUFFIX", schema: "PERSON");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnName("suffix_id")
                .IsRequired();

            builder.Property(p => p.Code)
                .HasColumnName("code")
                .HasColumnType("char(6)")
                .IsRequired();
            builder.HasIndex(c => c.Code).IsUnique();

            builder.Property(p => p.Description)
                .HasColumnName("description")
                .HasColumnType("varchar(15)")
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
