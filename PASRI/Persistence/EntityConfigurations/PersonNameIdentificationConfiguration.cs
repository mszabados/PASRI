using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PASRI.Core.Domain;

namespace PASRI.Persistence.EntityConfigurations
{
    public class PersonNameIdentificationConfiguration : IEntityTypeConfiguration<PersonNameIdentification>
    {
        public void Configure(EntityTypeBuilder<PersonNameIdentification> builder)
        {
            builder.ToTable("PersonNameIdentification");

            builder.Property(p => p.DoDServicePersonDocumentID)
                .IsRequired();

            builder.HasOne<PersonIdentification>(pni => pni.PersonIdentification)
                .WithMany(pi => pi.PersonNameIdentifications)
                .HasForeignKey(pni => pni.PersonIdentificationId)
                .IsRequired();
        }
    }
}
