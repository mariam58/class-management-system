using ClassManagementSystem.Entities;
using ClassManagementSystem.Interfaces;

namespace ClassManagementSystem.Repositories
{
    public class StudentCourseRepository : GenericRepository<StudentCourseEntity>, IStudentCourseRepository
    {
        public StudentCourseRepository(FinalExamDbContext context) : base(context) { }

        public override async Task Add(StudentCourseEntity entity)
        {
            if (entity == null) return;
            try
            {
                await _context.Database.BeginTransactionAsync();
                await _context.Set<StudentCourseEntity>().AddAsync(entity);
                await _context.SaveChangesAsync();
                await _context.Database.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                await _context.Database.RollbackTransactionAsync();
                throw ex;
            }
        }

       

        public override async Task Delete(StudentCourseEntity entity)
        {
            if (entity == null) return;
            try
            {
                await _context.Database.BeginTransactionAsync();
                _context.Set<StudentCourseEntity>().Remove(entity);
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
