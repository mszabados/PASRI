using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PASRI.Core.Domain;

namespace PASRI.Persistence.EntityConfigurations
{
    public class ReferenceHairColorConfiguration : IEntityTypeConfiguration<ReferenceHairColor>
    {
        public void Configure(EntityTypeBuilder<ReferenceHairColor> builder)
        {
            builder.ToTable("ReferenceHairColor");
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
