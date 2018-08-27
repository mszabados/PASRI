using PASRI.Core;
using PASRI.Core.Repositories;
using PASRI.Persistence.Repositories;

namespace PASRI.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PasriDbContext _context;

        public UnitOfWork(PasriDbContext context)
        {
            _context = context;
            Persons = new PersonRepository(_context);
        }

        public IPersonRepository Persons { get; private set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
