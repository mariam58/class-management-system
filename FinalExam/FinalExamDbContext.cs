using FinalExam.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinalExam
{
    public class FinalExamDbContext : DbContext
    {
        public FinalExamDbContext(DbContextOptions<FinalExamDbContext> options) : base(options) { }
        DbSet<StudentEntity> Students { get; set; }
        DbSet<CourseEntity> Courses { get; set; }
        DbSet<StudentCourseEntity> StudentCourses { get; set; }
        DbSet<TeacherEntity> Teachers { get; set; }
    }
}
