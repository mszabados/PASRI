using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PASRI.Core.Domain;

namespace PASRI.Persistence.EntityConfigurations
{
    public class ReferenceTypeBloodConfiguration : IEntityTypeConfiguration<ReferenceTypeBlood>
    {
        public void Configure(EntityTypeBuilder<ReferenceTypeBlood> builder)
        {
            builder.ToTable("ReferenceTypeBlood");
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
