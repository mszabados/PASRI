using Microsoft.EntityFrameworkCore;
using PASRI.Core.Domain;
using PASRI.Persistence.EntityConfigurations;

namespace PASRI.Persistence
{
    public class PasriDbContext : DbContext
    {
        public PasriDbContext(DbContextOptions<PasriDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<PersonIdentification> PersonIdentifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PersonConfiguration());
            modelBuilder.ApplyConfiguration(new PersonIdentificationConfiguration());
            modelBuilder.ApplyConfiguration(new PersonNameIdentificationConfiguration());
            modelBuilder.ApplyConfiguration(new PersonLegalNameIdentificationConfiguration());
            modelBuilder.ApplyConfiguration(new ReferenceSuffixNameConfiguration());
            modelBuilder.ApplyConfiguration(new ReferenceCountryConfiguration());
            modelBuilder.ApplyConfiguration(new ReferenceStateConfiguration());
            modelBuilder.ApplyConfiguration(new ReferenceHairColorConfiguration());
            modelBuilder.ApplyConfiguration(new ReferenceEyeColorConfiguration());
            modelBuilder.ApplyConfiguration(new ReferenceTypeBloodConfiguration());
            modelBuilder.ApplyConfiguration(new ReferenceRaceDemographicConfiguration());
            modelBuilder.ApplyConfiguration(new ReferenceEthnicGroupDemographicConfiguration());
            modelBuilder.ApplyConfiguration(new ReferenceGenderDemographicConfiguration());
            modelBuilder.ApplyConfiguration(new ReferenceReligionDemographicConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
