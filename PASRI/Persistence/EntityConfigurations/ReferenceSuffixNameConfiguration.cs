using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PASRI.Core.Domain;

namespace PASRI.Persistence.EntityConfigurations
{
    /// <summary>
    /// Configures the database schema for the domain model
    /// <see cref="ReferenceSuffixName"/> for use with code-first migrations
    /// </summary>
    /// <remarks>
    /// Data annotations should not be added to <see cref="ReferenceSuffixName"/> so the code
    /// controlling database schema remains maintained only in this class.
    /// </remarks>
    public class ReferenceSuffixNameConfiguration : IEntityTypeConfiguration<ReferenceSuffixName>
    {
        public void Configure(EntityTypeBuilder<ReferenceSuffixName> builder)
        {
            builder.ToTable("ReferenceSuffixName");
            builder.HasKey(p => p.Code);
            builder.Property(p => p.Code)
                .HasColumnType("char(4)")
                .IsRequired();
            builder.Property(p => p.DisplayText)
                .HasColumnType("varchar(255)")
                .IsRequired();
            builder.Property(p => p.StartDate)
                .HasColumnType("datetime");
            builder.Property(p => p.EndDate)
                .HasColumnType("datetime");
        }
    }
}
