using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PASRI.Core.Domain;

namespace PASRI.Persistence.EntityConfigurations
{
    public class ReferenceGenderDemographicConfiguration : IEntityTypeConfiguration<ReferenceGenderDemographic>
    {
        public void Configure(EntityTypeBuilder<ReferenceGenderDemographic> builder)
        {
            builder.ToTable("ReferenceGenderDemographic");
            builder.HasKey(p => p.Code);
            builder.Property(p => p.Code)
                .HasMaxLength(1)
                .IsRequired();
            builder.Property(p => p.DisplayText)
                .HasMaxLength(255)
                .IsRequired();
        }
    }
}
