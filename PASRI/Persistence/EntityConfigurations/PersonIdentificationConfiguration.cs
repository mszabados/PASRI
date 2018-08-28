using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PASRI.Core.Domain;

namespace PASRI.Persistence.EntityConfigurations
{
    public class PersonIdentificationConfiguration : IEntityTypeConfiguration<PersonIdentification>
    {
        public void Configure(EntityTypeBuilder<PersonIdentification> builder)
        {
            builder.ToTable("PersonIdentification");

            builder.HasOne<Person>(pi => pi.Person)
                .WithMany(p => p.PersonIdentifications)
                .HasForeignKey(pi => pi.PersonId)
                .IsRequired();
        }
    }
}
