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
            builder.ToTable("RE_SUFFIX");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnName("country_id")
                .IsRequired();

            builder.Property(p => p.Code)
                .HasColumnName("code")
                .HasColumnType("char(6)")
                .IsRequired();
            builder.HasIndex(c => c.Code).IsUnique();

            builder.Property(p => p.Description)
                .HasColumnType("varchar(15)")
                .IsRequired();

            builder.Property(p => p.CreatedDate)
                .HasColumnType("datetime")
                .HasAnnotation("description", "created_date");

            builder.Property(p => p.CreatedBy)
                .HasColumnType("varchar(30)")
                .HasAnnotation("description", "created_by");

            builder.Property(p => p.ModifiedDate)
                .HasColumnType("datetime")
                .HasAnnotation("description", "modified_date");

            builder.Property(p => p.ModifiedBy)
                .HasColumnType("varchar(30)")
                .HasAnnotation("description", "modified_by");
        }
    }
}
