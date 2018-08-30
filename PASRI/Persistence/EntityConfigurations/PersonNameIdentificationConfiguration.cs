using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PASRI.Core.Domain;

namespace PASRI.Persistence.EntityConfigurations
{
    /// <summary>
    /// Configures the database schema for the domain model
    /// <see cref="PersonNameIdentification"/> for use with code-first migrations
    /// </summary>
    /// <remarks>
    /// Data annotations should not be added to <see cref="PersonNameIdentification"/> so the code
    /// controlling database schema remains maintained only in this class.
    /// </remarks>
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
