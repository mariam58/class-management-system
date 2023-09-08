namespace FinalExam.Entities
{
    public class TeacherEntity : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Experience { get; set; }
        public virtual List<CourseEntity> Courses { get; set; }
    }
}
