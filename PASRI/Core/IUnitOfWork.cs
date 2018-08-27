using System;
using PASRI.Core.Repositories;

namespace PASRI.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IPersonRepository Persons { get; }
        int Complete();
    }
}
