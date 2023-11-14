using ClassManagementSystem.Entities;
using ClassManagementSystem.Interfaces;

namespace ClassManagementSystem.Repositories
{
    public class StudentRepository : GenericRepository<StudentEntity>, IStudentRepository
    {
        public StudentRepository(FinalExamDbContext context) : base(context) { }

        public override async Task Add(StudentEntity entity)
        {
            if (entity == null) return;
            try
            {
                await _context.Database.BeginTransactionAsync();
                await _context.Set<StudentEntity>().AddAsync(entity);
                await _context.SaveChangesAsync();
                await _context.Database.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                await _context.Database.RollbackTransactionAsync();
                throw ex;
            }
        }

        public override async Task Update(StudentEntity entity)
        {
            if (entity == null) return;
            try
            {
                await _context.Database.BeginTransactionAsync();
                _context.Set<StudentEntity>().Update(entity);
                await _context.SaveChangesAsync();
                await _context.Database.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                await _context.Database.RollbackTransactionAsync();
                throw ex;
            }
        }

        public override async Task Delete(StudentEntity entity)
        {
            if (entity == null) return;
            try
            {
                await _context.Database.BeginTransactionAsync();
                _context.Set<StudentEntity>().Remove(entity);
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
