using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PASRI.Core.Domain;

namespace PASRI.Persistence.EntityConfigurations
{
    public class ReferenceSuffixNameConfiguration : IEntityTypeConfiguration<ReferenceSuffixName>
    {
        public void Configure(EntityTypeBuilder<ReferenceSuffixName> builder)
        {
            builder.ToTable("ReferenceSuffixName");
            builder.HasKey(p => p.Code);
            builder.Property(p => p.Code)
                .HasMaxLength(4)
                .IsRequired();
        }
    }
}
