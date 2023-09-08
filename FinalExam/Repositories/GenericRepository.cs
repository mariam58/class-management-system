using FinalExam.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FinalExam.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly FinalExamDbContext _context;
        public GenericRepository(FinalExamDbContext context)
        {
            _context = context;
        }

        public virtual async Task<bool> Any(Expression<Func<T, bool>> predicate) => await _context.Set<T>().AnyAsync(predicate);
        public virtual async Task<T?> GetById(Guid Id) => await _context.Set<T>().FindAsync(Id);
        public virtual async Task<IEnumerable<T>> GetAllAsync() => await _context.Set<T>().ToListAsync();
        public virtual IQueryable<T> GetAll() => _context.Set<T>();
        public virtual async Task Add(T entity)
        {
            if (entity == null) return;
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public virtual async Task Update(T entity)
        {
            if (entity == null) return;
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }
        public virtual async Task Delete(T entity)
        {
            if (entity == null) return;
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
