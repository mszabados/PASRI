using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PASRI.API.Core.Domain;

namespace PASRI.API.Persistence.EntityConfigurations
{
    /// <summary>
    /// Configures the database schema for the domain model
    /// <see cref="PersonLegalNameIdentification"/> for use with code-first migrations
    /// </summary>
    /// <remarks>
    /// Data annotations should not be added to <see cref="PersonLegalNameIdentification"/> so the code
    /// controlling database schema remains maintained only in this class.
    /// </remarks>
    public class PersonLegalNameIdentificationConfiguration : IEntityTypeConfiguration<PersonLegalNameIdentification>
    {
        public void Configure(EntityTypeBuilder<PersonLegalNameIdentification> builder)
        {
            builder.ToTable("PersonLegalNameIdentification");

            builder.Property(p => p.First)
                .HasColumnType("varchar(255)");
            builder.Property(p => p.Middle)
                .HasColumnType("varchar(255)");
            builder.Property(p => p.Last)
                .HasColumnType("varchar(255)");
            builder.Property(p => p.ReferenceSuffixNameCode)
                .HasColumnType("char(4)");
            builder.Property(p => p.Full)
                .HasColumnType("varchar(255)");
            builder.Property(p => p.EffectiveDate)
                .HasColumnType("datetime");

            builder.HasOne(plni => plni.PersonNameIdentification)
                .WithMany(pni => pni.PersonLegalNameIdentifications)
                .HasForeignKey(plni => plni.PersonNameIdentificationId)
                .IsRequired();

            builder.HasOne(plni => plni.ReferenceSuffixName)
                .WithOne(rsn => rsn.PersonLegalNameIdentification)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
