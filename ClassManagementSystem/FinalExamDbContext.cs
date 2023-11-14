using ClassManagementSystem.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClassManagementSystem
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
