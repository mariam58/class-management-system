using FinalExam.Entities;
using FinalExam.Interfaces;

namespace FinalExam.Repositories
{
    public class StudentCourseRepository : GenericRepository<StudentCourseEntity>, IStudentCourseRepository
    {
        public StudentCourseRepository(FinalExamDbContext context) : base(context) { }
    }
}
