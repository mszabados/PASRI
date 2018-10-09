using HRC.DB.Master.Core.Domain;
using HRC.DB.Master.Persistence.Repositories;
using System.Collections.Generic;

namespace HRC.DB.Master.Core.Repositories
{
    /// <summary>
    /// Extends the <see cref="IRepository{TEntity}"/> for <see cref="Person"/>
    /// to minimize duplicate fat query logic (for example: get top 10 records order by a column).
    ///
    /// This should be included in an <see cref="IUnitOfWork"/> interface for <see cref="Persistence.MasterDbContext"/>
    /// </summary>
    /// <remarks>
    /// EntityFramework (DbSet) does not meet the Repository pattern on its own due to the need
    /// to write fat queries multiple times in the application code, therefore, to minimize
    /// this query logic, it should be abstracted to a repository.
    /// </remarks>
    public interface IPersonRepository : IRepository<Person>
    {
        /// <summary>
        /// Returns an eager loaded <see cref="Person"/> object
        /// </summary>
        /// <param name="personId">Unique <see cref="Person"/> identification number</param>
        /// <returns><see cref="Person"/></returns>
        Person GetEagerLoadedPerson(int personId);

        /// <summary>
        /// Searches and selects from the database a <see cref="Person"/> collection
        /// based on input parameters
        /// </summary>
        /// <param name="personId">Optional <see cref="Person"/> identification number</param>
        /// <param name="includeInfo">Optional list of include objects corresponding with
        /// the IncludeInfo constants of <see cref="PersonRepository"/></param>
        /// <returns><see cref="List{Person}"/></returns>
        List<Person> Search(int? personId, List<string> includeInfo);
    }
}
