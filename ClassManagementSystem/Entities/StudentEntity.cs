namespace ClassManagementSystem.Entities
{
    public class StudentEntity : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public virtual List<StudentCourseEntity> Courses { get; set; }
    }
}
