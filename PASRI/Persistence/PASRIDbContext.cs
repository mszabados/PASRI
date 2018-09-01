using Microsoft.EntityFrameworkCore;
using PASRI.API.Core.Domain;
using PASRI.API.Persistence.EntityConfigurations;

namespace PASRI.API.Persistence
{
    /// <summary>
    /// The <see cref="DbContext"/> for the project which exposes <see cref="DbSet{TEntity}"/>
    /// for schema and CRUD operations to the database.  Schema configurations for each
    /// <see cref="DbSet{TEntity}"/> are defined in separate classes.
    /// </summary>
    /// <remarks>
    /// Properties should be named in plural form.
    /// </remarks>
    public class PasriDbContext : DbContext
    {
        public PasriDbContext(DbContextOptions<PasriDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<PersonIdentification> PersonIdentifications { get; set; }
        public virtual DbSet<ReferenceCountry> ReferenceCountries{ get; set; }
        public virtual DbSet<ReferenceEthnicGroupDemographic> ReferenceEthnicGroupDemographics { get; set; }
        public virtual DbSet<ReferenceEyeColor> ReferenceEyeColors { get; set; }
        public virtual DbSet<ReferenceHairColor> ReferenceHairColors { get; set; }
        public virtual DbSet<ReferenceRaceDemographic> ReferenceRaceDemographics { get; set; }
        public virtual DbSet<ReferenceReligionDemographic> ReferenceReligionDemographics { get; set; }
        public virtual DbSet<ReferenceState> ReferenceStates { get; set; }
        public virtual DbSet<ReferenceSuffixName> ReferenceSuffixNames { get; set; }
        public virtual DbSet<ReferenceBloodType> ReferenceTypeBloods { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PersonConfiguration());
            modelBuilder.ApplyConfiguration(new PersonIdentificationConfiguration());
            modelBuilder.ApplyConfiguration(new PersonNameIdentificationConfiguration());
            modelBuilder.ApplyConfiguration(new PersonLegalNameIdentificationConfiguration());
            modelBuilder.ApplyConfiguration(new ReferenceCountryConfiguration());
            modelBuilder.ApplyConfiguration(new ReferenceEthnicGroupDemographicConfiguration());
            modelBuilder.ApplyConfiguration(new ReferenceEyeColorConfiguration());
            modelBuilder.ApplyConfiguration(new ReferenceGenderDemographicConfiguration());
            modelBuilder.ApplyConfiguration(new ReferenceHairColorConfiguration());
            modelBuilder.ApplyConfiguration(new ReferenceRaceDemographicConfiguration());
            modelBuilder.ApplyConfiguration(new ReferenceReligionDemographicConfiguration());
            modelBuilder.ApplyConfiguration(new ReferenceStateConfiguration());
            modelBuilder.ApplyConfiguration(new ReferenceSuffixNameConfiguration());
            modelBuilder.ApplyConfiguration(new ReferenceBloodTypeConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
