using HRC.DB.Master.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRC.DB.Master.Persistence.EntityConfigurations
{
    /// <summary>
    /// Configures the database schema for the domain model
    /// <see cref="Person"/> for use with code-first migrations
    /// </summary>
    /// <remarks>
    /// Data annotations should not be added to <see cref="Person"/> so the code
    /// controlling database schema remains maintained only in this class.
    /// </remarks>
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public const int FirstNameLength = 80;
        public const int MiddleNameLength = 80;
        public const int LastNameLength = 80;

        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("PERSON", "PERSON");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnName("person_id")
                .IsRequired();

            builder.Property(p => p.FirstName)
                .HasColumnName("first_name")
                .HasColumnType($"varchar({FirstNameLength})")
                .IsRequired();

            builder.Property(p => p.MiddleName)
                .HasColumnName("middle_name")
                .HasColumnType($"varchar({MiddleNameLength})");

            builder.Property(p => p.LastName)
                .HasColumnName("last_name")
                .HasColumnType($"varchar({LastNameLength})")
                .IsRequired();

            builder.Property(p => p.SuffixId)
                .HasColumnName("re_suffix_id");

            builder.Property(p => p.EffectiveDate)
                .HasColumnName("effect_date")
                .HasColumnType("date");

            builder.Property(p => p.CreatedDate)
                .HasColumnName("created_date")
                .HasColumnType("datetime");

            builder.Property(p => p.CreatedBy)
                .HasColumnName("created_by")
                .HasColumnType("varchar(30)");

            builder.Property(p => p.ModifiedDate)
                .HasColumnName("last_modified_date")
                .HasColumnType("datetime");

            builder.Property(p => p.ModifiedBy)
                .HasColumnName("last_modified_by")
                .HasColumnType("varchar(30)");

            builder.HasOne(p => p.Suffix)
                .WithMany()
                .HasForeignKey(p => p.SuffixId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
