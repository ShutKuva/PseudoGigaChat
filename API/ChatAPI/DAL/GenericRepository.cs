using Core.BaseEntities;
using DAL.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DAL
{
    public class GenericRepository<T> : IRepository<T> where T: BaseEntity
    {
        protected readonly DbContext _context;

        public GenericRepository(DbContext dbContext)
        {
            _context = dbContext;
        }

        public virtual Task Create(T entity)
        {
            return Task.Run(() =>
            {
                _context.Set<T>().Add(entity);
                _context.SaveChanges();
            });
        }

        public virtual Task<T> Read(int id)
        {
            return Task.Run(() =>
            {
                T result = _context.Set<T>().FirstOrDefault(entity => entity.Id == id);
                if (result == null)
                {
                    throw new ArgumentException(nameof(id));
                }

                return result;
            });
        }

        public virtual Task<List<T>> ReadByCondition(Expression<Func<T, bool>> condition)
        {
            return Task.Run(() => _context.Set<T>().Where(condition).ToList());
        }

        public virtual async Task Edit(T entity)
        {
            T actualEntity = await Read(entity.Id);

            _context.Entry(actualEntity).CurrentValues.SetValues(entity);
        }

        public virtual async Task Delete(int id)
        {
            T entity = await Read(id);

            _context.Remove(entity);

            _context.SaveChanges();
        }

        public Task<List<T>> ReadAll()
        {
            return Task.Run(() => _context.Set<T>().ToList());
        }

        public virtual async Task<List<T>> ReadFromTo(int number, int from = 0, Expression<Func<T, bool>> condition = null, Comparison<T> comparator = null)
        {
            List<T> result = await ReadByCondition(condition ?? (entity => true));

            result.Sort(comparator ?? ((x, y) => 0));

            if (from == 0)
            {
                return result.Take(number).ToList();
            }
            else
            {
                return result.Skip(from).Take(number).ToList();
            }
        }

        public Task<int> Count(Expression<Func<T, bool>> condition)
        {
            return Task.Run(() => _context.Set<T>().Where(condition).Count());
        }
    }
}
