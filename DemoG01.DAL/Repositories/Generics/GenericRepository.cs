using DemoG01.DAL.Data.Contexts;
using DemoG01.DAL.Models;
using DemoG01.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DemoG01.DAL.Repositories.Generics
{
    public class GenericRepository<TEntity> (ApplicationDbContext _dbContext): IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        public IEnumerable<TEntity> GetAll(bool withTracking = false)
        {
            if (withTracking)
            {
                return _dbContext.Set<TEntity>().Where(T => T.IsDeleted != true).ToList();
            }
            else
            {
                return _dbContext.Set<TEntity>().Where(T => T.IsDeleted != true).AsNoTracking().ToList();
            }
        }
        public IEnumerable<TResult> GetAll<TResult>(Expression<Func<TEntity, TResult>> Selector)
        {
            return _dbContext.Set<TEntity>().Where(entity => entity.IsDeleted != true)
                .Select(Selector).ToList();
        }
        //Get By Id
        public TEntity? GetById(int Id) => _dbContext.Set<TEntity>().Find(Id);
        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbContext.Set<TEntity>().Where(predicate).ToList();
        }
        //Insert
        public void Add(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
        }

        //Update
        public void Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
        }

        //Remove
        public void Remove(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }

       
    }
}
