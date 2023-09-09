using FinalExam.Entities;
using FinalExam.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace FinalExam.Controllers
{
    [Controller]
    [Route("[Controller]")]
    public class StudentCourseController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly FinalExamDbContext _context;
        public StudentCourseController(IUnitOfWork uow, FinalExamDbContext context)
        {
            _uow = uow;
            _context = context;
        }

        [HttpGet("GetStudentCourses")]
        public async Task<IActionResult> GetStudentCourses()
        {
            var data = await _uow.studentCourseRepository.GetAllAsync();
            return Ok(data);
        }

        [HttpPost("AddStudentCourse")]
        public async Task<IActionResult> AddStudentCourse(Guid studentId, Guid courseId)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();
                var studentCourse = new StudentCourseEntity()
                {
                    StudentId = studentId,
                    CourseId = courseId
                };

                await _uow.studentCourseRepository.Add(studentCourse);
                await _context.Database.CommitTransactionAsync();
                return Ok(studentCourse);
            }
            catch (Exception ex)
            {
                await _context.Database.RollbackTransactionAsync();
                throw ex;
            }
        }

        [HttpDelete("DeleteStudentCourse")]
        public async Task<IActionResult> DeleteStudentCourse(Guid id)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();
                var studentCourse = await _uow.studentCourseRepository.GetById(id);
                if (studentCourse == null) throw new Exception("null");
                await _uow.studentCourseRepository.Delete(studentCourse);
                await _context.Database.CommitTransactionAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                await _context.Database.RollbackTransactionAsync();
                throw ex;
            }
        }
    }
}
