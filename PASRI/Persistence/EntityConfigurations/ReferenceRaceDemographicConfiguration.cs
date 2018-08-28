using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PASRI.Core.Domain;

namespace PASRI.Persistence.EntityConfigurations
{
    public class ReferenceRaceDemographicConfiguration : IEntityTypeConfiguration<ReferenceRaceDemographic>
    {
        public void Configure(EntityTypeBuilder<ReferenceRaceDemographic> builder)
        {
            builder.ToTable("ReferenceRaceDemographic");
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
