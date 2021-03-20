using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Company.Finance.Persistence
{
    public interface IUnitOfWork
    {
        Task<IQueryable<TEntity>> FindAll<TEntity>(Expression<Func<TEntity, bool>> expression) 
            where TEntity : class;
        Task Add<TEntity>(TEntity entity);
        Task AddMany<TEntity>(IEnumerable<TEntity> entities);
        Task Commit();
    }

    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly DbContext _context;

        public UnitOfWork(DbContext context)
        {
            _context = context;
        }

        public async Task<IQueryable<TEntity>> FindAll<TEntity>(Expression<Func<TEntity, bool>> expression) 
            where TEntity : class
        {
            var all = _context.Set<TEntity>();
            return all.Where(expression);
        }

        public async Task Add<TEntity>(TEntity entity)
        {
            await _context.AddAsync(entity);
        }

        public async Task AddMany<TEntity>(IEnumerable<TEntity> entities)
        {
            await Task.WhenAll(entities.Select(async entity => await _context.AddAsync(entity)));
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context?.
                Dispose();
        }
    }
}