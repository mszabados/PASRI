using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PASRI.Core.Domain;

namespace PASRI.Persistence.EntityConfigurations
{
    /// <summary>
    /// Configures the database schema for the domain model
    /// <see cref="PersonIdentification"/> for use with code-first migrations
    /// </summary>
    /// <remarks>
    /// Data annotations should not be added to <see cref="PersonIdentification"/> so the code
    /// controlling database schema remains maintained only in this class.
    /// </remarks>
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
