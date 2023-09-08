using FinalExam.Entities;
using FinalExam.Interfaces;

namespace FinalExam.Repositories
{
    public class TeacherRepository : GenericRepository<TeacherEntity>, ITeacherRepository
    {
        public TeacherRepository(FinalExamDbContext context) : base(context) { }
    }
}
