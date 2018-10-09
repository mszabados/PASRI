using HRC.DB.Master.Core.Domain;

namespace HRC.DB.Master.Core.Repositories
{
    /// <summary>
    /// Extends the <see cref="IRepository{TEntity}"/> for <see cref="Birth"/>
    /// to minimize duplicate fat query logic (for example: get top 10 records order by a column).
    ///
    /// This should be included in an <see cref="IUnitOfWork"/> interface for <see cref="Persistence.MasterDbContext"/>
    /// </summary>
    /// <remarks>
    /// EntityFramework (DbSet) does not meet the Repository pattern on its own due to the need
    /// to write fat queries multiple times in the application code, therefore, to minimize
    /// this query logic, it should be abstracted to a repository.
    /// </remarks>
    public interface IBirthRepository : IRepository<Birth>
    {
        /// <summary>
        /// Returns an eagerly loaded <see cref="Birth"/> object for updating by the API controller
        /// </summary>
        /// <param name="birthId">Unique <see cref="Birth"/> identification number</param>
        /// <returns><see cref="Birth"/></returns>
        Birth GetEagerLoadedBirthById(int birthId);

        /// <summary>
        /// Returns an eagerly loaded <see cref="Birth"/> object for updating by the API controller
        /// </summary>
        /// <param name="personId">Unique <see cref="Person"/> identification number</param>
        /// <returns><see cref="Birth"/></returns>
        Birth GetEagerLoadedBirthByPersonId(int personId);
    }
}
