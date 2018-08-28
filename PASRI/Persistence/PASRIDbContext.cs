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

            base.OnModelCreating(modelBuilder);
        }
    }
}
