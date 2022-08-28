using System.Linq.Expressions;

namespace DAL.Abstractions
{
    public interface IRepository<T>
    {
        public Task Create(T entity);

        public Task<T> Read(int id);

        public Task<List<T>> ReadByCondition(Expression<Func<T, bool>> condition);

        public Task<List<T>> ReadAll();

        public Task<List<T>> ReadFromTo(int number, int from = 0, Expression<Func<T, bool>> condition = null, Comparison<T> comparator = null);

        public Task Edit(T entity);

        public Task Delete(int id);

        public Task<int> Count(Expression<Func<T, bool>> condition);
    }
}
