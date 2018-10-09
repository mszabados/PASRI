using HRC.DB.Master.Core.Domain;
using HRC.DB.Master.Persistence.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace HRC.DB.Master.Persistence
{
    /// <summary>
    /// The <see cref="DbContext"/> for the project which exposes <see cref="DbSet{TEntity}"/>
    /// for schema and CRUD operations to the database.  Schema configurations for each
    /// <see cref="DbSet{TEntity}"/> are defined in separate classes.
    /// </summary>
    /// <remarks>
    /// Properties should be named in plural form.
    /// </remarks>
    [ExcludeFromCodeCoverage]
    public class MasterDbContext : DbContext
    {
        protected MasterDbContext()
        {
        }

        // ReSharper disable once UnusedMember.Global
        [ExcludeFromCodeCoverage]
        public MasterDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public virtual DbSet<Birth> Births { get; set; }
        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<ReferenceCountry> ReferenceCountries { get; set; }
        public virtual DbSet<ReferenceEthnicGroup> ReferenceEthnicGroups { get; set; }
        public virtual DbSet<ReferenceGender> ReferenceGenders { get; set; }
        public virtual DbSet<ReferenceEyeColor> ReferenceEyeColors { get; set; }
        public virtual DbSet<ReferenceHairColor> ReferenceHairColors { get; set; }
        public virtual DbSet<ReferenceNameSuffix> ReferenceNameSuffixes { get; set; }
        public virtual DbSet<ReferenceRace> ReferenceRaces { get; set; }
        public virtual DbSet<ReferenceReligion> ReferenceReligions { get; set; }
        public virtual DbSet<ReferenceStateProvince> ReferenceStates { get; set; }
        public virtual DbSet<ReferenceBloodType> ReferenceBloodTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BirthConfiguration());
            modelBuilder.ApplyConfiguration(new PersonConfiguration());
            modelBuilder.ApplyConfiguration(new ReferenceBloodTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ReferenceCountryConfiguration());
            modelBuilder.ApplyConfiguration(new ReferenceEthnicGroupConfiguration());
            modelBuilder.ApplyConfiguration(new ReferenceEyeColorConfiguration());
            modelBuilder.ApplyConfiguration(new ReferenceGenderConfiguration());
            modelBuilder.ApplyConfiguration(new ReferenceHairColorConfiguration());
            modelBuilder.ApplyConfiguration(new ReferenceNameSuffixConfiguration());
            modelBuilder.ApplyConfiguration(new ReferenceRaceConfiguration());
            modelBuilder.ApplyConfiguration(new ReferenceReligionConfiguration());
            modelBuilder.ApplyConfiguration(new ReferenceStateProvinceConfiguration());
            

            base.OnModelCreating(modelBuilder);
        }
    }
}
