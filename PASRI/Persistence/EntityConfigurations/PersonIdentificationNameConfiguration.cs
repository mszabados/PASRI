using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PASRI.Core.Domain;

namespace PASRI.Persistence.EntityConfigurations
{
    public class PersonIdentificationNameConfiguration : IEntityTypeConfiguration<PersonIdentificationName>
    {
        public void Configure(EntityTypeBuilder<PersonIdentificationName> builder)
        {
            builder.ToTable("PersonIdentificationName");

            builder.Property(p => p.First)
                .HasMaxLength(255);
            builder.Property(p => p.Middle)
                .HasMaxLength(255);
            builder.Property(p => p.Last)
                .HasMaxLength(255);
            builder.Property(p => p.Full)
                .HasMaxLength(255);

            builder.HasOne<PersonIdentification>(pin => pin.PersonIdentification)
                .WithOne(pi => pi.PersonIdentificationName)
                .IsRequired();
        }
    }
}
