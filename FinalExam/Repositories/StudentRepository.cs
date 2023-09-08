using FinalExam.Entities;
using FinalExam.Interfaces;

namespace FinalExam.Repositories
{
    public class StudentRepository : GenericRepository<StudentEntity>, IStudentRepository
    {
        public StudentRepository(FinalExamDbContext context) : base(context) { }
    }
}
