using BLL.Abstractions;
using Core.BaseEntities;
using DAL.Abstractions;
using System.Linq.Expressions;

namespace BLL
{
    public class CRUDService<T> : ICRUDService<T> where T : BaseEntity
    {
        protected readonly IRepository<T> _repository;

        public CRUDService(IRepository<T> repository)
        {
            _repository = repository;
        }

        public Task<int> Count(Expression<Func<T, bool>> condition)
        {
            return _repository.Count(condition);
        }

        public virtual Task Create(T entity)
        {
            return _repository.Create(entity);
        }

        public virtual Task Delete(int id)
        {
            return _repository.Delete(id);
        }

        public virtual Task Edit(T updatedEntity)
        {
            return _repository.Edit(updatedEntity);
        }

        public virtual Task<T> Get(int id)
        {
            return _repository.Read(id);
        }

        public Task<List<T>> GetAll()
        {
            return _repository.ReadAll();
        }

        public Task<List<T>> GetByCondition(Expression<Func<T, bool>> condition)
        {
            return _repository.ReadByCondition(condition);
        }

        public Task<List<T>> GetNumberOf(int number, int from = 0, Expression<Func<T, bool>> condition = null, Comparison<T> comparator = null)
        {
            return _repository.ReadFromTo(number, from, condition, comparator);
        }
    }
}
