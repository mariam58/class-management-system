namespace FinalExam.Interfaces
{
    public interface IUnitOfWork
    {
        IStudentRepository studentRepository { get; }
        ICourseRepository courseRepository { get; }
        ITeacherRepository teacherRepository { get; }
    }
}
