using System.Linq.Expressions;

namespace BLL.Abstractions
{
    public interface ICRUDService<T>
    {
        Task Create(T entity);
        Task<T> Get(int id);
        Task<List<T>> GetByCondition(Expression<Func<T, bool>> condition);
        Task<List<T>> GetAll();
        Task<List<T>> GetNumberOf(int number, int from = 0, Expression<Func<T, bool>> condition = null, Comparison<T> comparator = null);
        Task Edit(T updatedEntity);
        Task Delete(int id);
        Task<int> Count(Expression<Func<T, bool>> condition);
    }
}
