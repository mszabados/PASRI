﻿using HRC.DB.Master.Core.Repositories;
using HRC.Helper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace HRC.DB.Master.Persistence.Repositories
{
    /// <summary>
    /// Base generic repository class taking in any base DbContext to perform its work
    /// </summary>
    /// <remarks>
    /// A repository should act like a collection of objects in memory and not an extension of the database.
    /// There should be no Update methods.  If you want to update an object, get it from a collection and
    /// change it in the database with the unit of work pattern.
    /// 
    /// The main benefits of the repository and unit of work pattern is to create an abstraction
    /// layer between the data access/persistence layer and the business logic/application layer.
    /// It minimizes duplicate query logic and promotes testability (unit tests or TDD)
    /// 
    /// See also Patterns of Enterprise Application Architecture from Martin Fowler
    /// https://www.martinfowler.com/eaaCatalog/repository.html
    /// </remarks>
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;
        protected const string IncludeInfoConstantSuffix = "Info";

        private readonly DbSet<TEntity> _entities;

        protected Repository(DbContext context)
        {
            Context = context;

            // Here we are working with a DbContext, not PasriContext. Since we don't have DbSets 
            // we need to use the generic Set() method to access them.
            _entities = Context.Set<TEntity>();
        }

        public virtual TEntity Get(int id)
        {
            
            return _entities.Find(id);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return _entities.ToList();
        }

        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.Where(predicate);
        }

        public virtual TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.SingleOrDefault(predicate);
        }

        public virtual void Add(TEntity entity)
        {
            _entities.Add(entity);
        }

        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
            _entities.AddRange(entities);
        }

        public virtual void Remove(TEntity entity)
        {
            _entities.Remove(entity);
        }

        public virtual void RemoveRange(IEnumerable<TEntity> entities)
        {
            _entities.RemoveRange(entities);
        }

        public List<string> GetAllIncludeInfoConstants() =>
            Utils.GetAllConstantsEndsWith(GetType(), IncludeInfoConstantSuffix)
                .Select(fi => fi.GetValue(null).ToString())
                .ToList();
    }
}
