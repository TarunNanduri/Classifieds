﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Classifieds.App.Services.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Classifieds.App.Services.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly ClassifiedsContext _context;
        private readonly DbSet<TEntity> _entities;

        protected Repository(ClassifiedsContext context)
        {
            _context = context;
            _entities = context.Set<TEntity>();
        }

        public TEntity Get(int id)
        {
            return _entities.Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _entities.ToList();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.Where(predicate);
        }

        public void Add(TEntity entity)
        {
            _entities.Add(entity);
            _context.SaveChanges();
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _entities.AddRange(entities);
            _context.SaveChanges();
        }

        public void Remove(TEntity entity)
        {
            _entities.Remove(entity);
            _context.SaveChanges();
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _entities.RemoveRange(entities);
            _context.SaveChanges();
        }

        public void Update(TEntity entity, int id)
        {
            var item = _entities.Find(id);
            var entryItem = _context.Entry(item);
            entryItem.CurrentValues.SetValues(entity);
            _context.SaveChanges();
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.SingleOrDefault(predicate);
        }
    }
}