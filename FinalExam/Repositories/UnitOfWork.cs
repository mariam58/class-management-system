using FinalExam.Interfaces;

namespace FinalExam.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private IServiceProvider _serviceProvider;
        public UnitOfWork(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            _serviceProvider = serviceProvider;
        }
        public IStudentRepository studentRepository => _serviceProvider.GetRequiredService<IStudentRepository>();
        public ICourseRepository courseRepository => _serviceProvider.GetRequiredService<ICourseRepository>();
        public ITeacherRepository teacherRepository => _serviceProvider.GetRequiredService<ITeacherRepository>();
    }
}
