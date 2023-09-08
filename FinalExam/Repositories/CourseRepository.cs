using FinalExam.Entities;
using FinalExam.Interfaces;

namespace FinalExam.Repositories
{
    public class CourseRepository : GenericRepository<CourseEntity>, ICourseRepository
    {
        public CourseRepository(FinalExamDbContext context) : base(context) { }
    }
}
