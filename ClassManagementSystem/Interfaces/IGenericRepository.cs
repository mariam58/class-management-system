using System.Linq.Expressions;

namespace ClassManagementSystem.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<bool> Any(Expression<Func<T, bool>> predicate);
        Task<T?> GetById(Guid Id);
        IQueryable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);
    }
}
