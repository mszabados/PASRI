using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PASRI.Core.Domain;

namespace PASRI.Persistence.EntityConfigurations
{
    /// <summary>
    /// Configures the database schema for the domain model
    /// <see cref="ReferenceTypeBloodConfiguration"/> for use with code-first migrations
    /// </summary>
    /// <remarks>
    /// Data annotations should not be added to <see cref="ReferenceTypeBloodConfiguration"/> so the code
    /// controlling database schema remains maintained only in this class.
    /// </remarks>
    public class ReferenceTypeBloodConfiguration : IEntityTypeConfiguration<ReferenceTypeBlood>
    {
        public void Configure(EntityTypeBuilder<ReferenceTypeBlood> builder)
        {
            builder.ToTable("ReferenceTypeBlood");
            builder.HasKey(p => p.Code);
            builder.Property(p => p.Code)
                .HasColumnType("char(1)")
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
