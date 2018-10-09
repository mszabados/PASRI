using HRC.DB.Master.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRC.DB.Master.Persistence.EntityConfigurations
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
        public const int BirthCityLength = 100;

        public void Configure(EntityTypeBuilder<Birth> builder)
        {
            builder.ToTable("BIRTH", "PERSON");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnName("birth_id")
                .IsRequired();

            builder.Property(p => p.PersonId)
                .HasColumnName("person_id");

            builder.Property(p => p.Date)
                .HasColumnName("birth_date")
                .HasColumnType("date");

            builder.Property(p => p.City)
                .HasColumnName("birth_city")
                .HasColumnType($"varchar({BirthCityLength})");

            builder.Property(p => p.StateProvinceId)
                .HasColumnName("state_province_id")
                .HasColumnType("int");

            builder.Property(p => p.CountryId)
                .HasColumnName("country_id")
                .HasColumnType("int");

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

            builder.HasOne(b => b.Person)
                .WithOne(p => p.Birth)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(b => b.StateProvince)
                .WithMany()
                .HasForeignKey(b => b.StateProvinceId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(b => b.Country)
                .WithMany()
                .HasForeignKey(b => b.CountryId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
