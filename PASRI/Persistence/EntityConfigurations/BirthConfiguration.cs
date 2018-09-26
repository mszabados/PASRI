using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PASRI.API.Core.Domain;
using PASRI.API.Migrations;

namespace PASRI.API.Persistence.EntityConfigurations
{
    /// <summary>
    /// Configures the database schema for the domain model
    /// <see cref="Birth"/> for use with code-first migrations
    /// </summary>
    /// <remarks>
    /// Data annotations should not be added to <see cref="Birth"/> so the code
    /// controlling database schema remains maintained only in this class.
    /// </remarks>
    public class BirthConfiguration : IEntityTypeConfiguration<Birth>
    {
        public void Configure(EntityTypeBuilder<Birth> builder)
        {
            builder.ToTable("BIRTH");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnName("birth_id")
                .IsRequired();

            builder.Property(p => p.PersonId)
                .HasColumnName("person_id");

            builder.Property(p => p.Date)
                .HasColumnName("birth_date")
                .HasColumnType("date")
                .IsRequired();

            builder.Property(p => p.City)
                .HasColumnName("birth_city")
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(p => p.StateProvinceId)
                .HasColumnName("state_province_id")
                .HasColumnType("int");

            builder.Property(p => p.CountryId)
                .HasColumnName("country_id")
                .HasColumnType("int")
                .IsRequired();

            builder.Property(p => p.CreatedDate)
                .HasColumnName("created_date")
                .HasColumnType("datetime");

            builder.Property(p => p.CreatedBy)
                .HasColumnName("created_by")
                .HasColumnType("varchar(30)");

            builder.Property(p => p.ModifiedDate)
                .HasColumnName("modified_date")
                .HasColumnType("datetime");

            builder.Property(p => p.ModifiedBy)
                .HasColumnName("modified_by")
                .HasColumnType("varchar(30)");

            builder.HasOne<Person>(b => b.Person)
                .WithOne(p => p.Birth)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<ReferenceStateProvince>(b => b.StateProvince)
                .WithMany()
                .HasForeignKey(b => b.StateProvinceId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<ReferenceCountry>(b => b.Country)
                .WithMany()
                .HasForeignKey(b => b.CountryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
