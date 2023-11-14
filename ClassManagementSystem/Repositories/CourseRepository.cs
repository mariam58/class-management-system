using ClassManagementSystem;
using ClassManagementSystem.Entities;
using ClassManagementSystem.Interfaces;

namespace ClassManagementSystem.Repositories
{
    public class CourseRepository : GenericRepository<CourseEntity>, ICourseRepository
    {
        public CourseRepository(FinalExamDbContext context) : base(context) { }

        public override async Task Add(CourseEntity entity)
        {
            if (entity == null) return;
            try
            {
                await _context.Database.BeginTransactionAsync();
                await _context.Set<CourseEntity>().AddAsync(entity);
                await _context.SaveChangesAsync();
                await _context.Database.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                await _context.Database.RollbackTransactionAsync();
                throw ex;
            }
        }

        public override async Task Update(CourseEntity entity)
        {
            if (entity == null) return;
            try
            {
                await _context.Database.BeginTransactionAsync();
                _context.Set<CourseEntity>().Update(entity);
                await _context.SaveChangesAsync();
                await _context.Database.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                await _context.Database.RollbackTransactionAsync();
                throw ex;
            }
        }
    }
}
