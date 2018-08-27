using PASRI.Core.Domain;

namespace PASRI.Core.Repositories
{
    public interface IPersonRepository : IRepository<Person>
    {
        Person GetPersonByIdentificationNumber(int personIdentificationId);
    }
}
