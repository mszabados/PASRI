using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PASRI.Core.Domain;

namespace PASRI.Persistence.EntityConfigurations
{
    public class PersonLegalNameIdentificationConfiguration : IEntityTypeConfiguration<PersonLegalNameIdentification>
    {
        public void Configure(EntityTypeBuilder<PersonLegalNameIdentification> builder)
        {
            builder.ToTable("PersonLegalNameIdentification");

            builder.Property(p => p.First)
                .HasMaxLength(255);
            builder.Property(p => p.Middle)
                .HasMaxLength(255);
            builder.Property(p => p.Last)
                .HasMaxLength(255);
            builder.Property(p => p.Full)
                .HasMaxLength(255);

            builder.HasOne<PersonNameIdentification>(plni => plni.PersonNameIdentification)
                .WithMany(pni => pni.PersonLegalNameIdentifications)
                .HasForeignKey(plni => plni.PersonNameIdentificationId)
                .IsRequired();
        }
    }
}
