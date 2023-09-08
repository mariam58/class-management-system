namespace FinalExam.Entities
{
    public class CourseEntity : BaseEntity
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public virtual List<StudentCourseEntity> Students { get; set; }
        public Guid TeacherId { get; set; }
        public virtual TeacherEntity Teacher { get; set; }
    }
}
