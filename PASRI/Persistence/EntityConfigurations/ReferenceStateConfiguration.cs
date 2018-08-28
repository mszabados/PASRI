using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PASRI.Core.Domain;

namespace PASRI.Persistence.EntityConfigurations
{
    public class ReferenceStateConfiguration : IEntityTypeConfiguration<ReferenceState>
    {
        public void Configure(EntityTypeBuilder<ReferenceState> builder)
        {
            builder.ToTable("ReferenceState");
            builder.HasKey(p => p.Code);
            builder.Property(p => p.Code)
                .HasMaxLength(2)
                .IsRequired();
            builder.Property(p => p.DisplayText)
                .HasMaxLength(255)
                .IsRequired();
        }
    }
}
