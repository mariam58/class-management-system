namespace ClassManagementSystem.Interfaces
{
    public interface IUnitOfWork
    {
        IStudentRepository studentRepository { get; }
        IStudentCourseRepository studentCourseRepository { get; }
        ICourseRepository courseRepository { get; }
        ITeacherRepository teacherRepository { get; }
    }
}
