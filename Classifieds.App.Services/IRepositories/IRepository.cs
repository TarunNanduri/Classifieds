﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Classifieds.App.Services.IRepositories
{
    public interface IRepository<TEntity> where TEntity : class

    {
        TEntity Get(int id);
        IEnumerable<TEntity> GetAll();

        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entity);

        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entity);

        void Update(TEntity entity, int id);
    }
}