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

            builder.HasOne<PersonNameIdentification>(plni => plni.PersonNameIdentification)
                .WithMany(pni => pni.PersonLegalNameIdentifications)
                .HasForeignKey(plni => plni.PersonNameIdentificationId)
                .IsRequired();

            builder.HasOne<ReferenceSuffixName>(plni => plni.ReferenceSuffixName)
                .WithOne(rsn => rsn.PersonLegalNameIdentification)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
