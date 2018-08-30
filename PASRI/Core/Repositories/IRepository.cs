using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace PASRI.Core.Repositories
{
    /// <summary>
    /// Provides generic methods for getting one or many repository objects,
    /// finding repository objects with a predicate, adding one or many repository
    /// objects to the collection, or removing one or many repository objects from the collection.
    ///
    /// A repository should act like a collection of objects in memory and not an extension of the database.
    /// There should be no Update methods.  If you want to update an object, get it from a collection and
    /// change it in the database with the unit of work pattern.
    /// </summary>
    /// <remarks>
    /// The main benefits of a repository and unit of work pattern is to create an abstraction
    /// layer between the data access/persistence layer and the business logic/application layer.
    /// It minimizes duplicate query logic and promotes testability (unit tests or TDD)
    /// </remarks>
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(int id);
        TEntity Get(string primaryKeyString);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}
