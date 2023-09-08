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
            var data = await _context.Set<StudentCourseEntity>()
                .ToListAsync();
            var settings = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.Indented
            };
            var parsedData = JsonConvert.SerializeObject(data, settings);
            return Ok(parsedData);
        }

        [HttpPost("AddStudentCourse")]
        public async Task AddStudentCourse(Guid studentId, Guid courseId)
        {
            var studentCourse = new StudentCourseEntity
            {
                StudentId = studentId,
                CourseId = courseId
            };
            await _context.AddAsync(studentCourse);
            await _context.SaveChangesAsync();
        }

        [HttpPut("EditStudentCourse")]
        public async Task<IActionResult> EditStudentCourse(Guid studentId, Guid courseId, Guid id)
        {
            var studentCourse = await _context.Set<StudentCourseEntity>().FirstOrDefaultAsync(x => x.Id == id);
            studentCourse.StudentId = studentId;
            studentCourse.CourseId = courseId;
            _context.Set<StudentCourseEntity>().Update(studentCourse);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("DeleteStudentCourse")]
        public async Task<IActionResult> DeleteStudentCourse(Guid id)
        {
            var studentCourse = await _context.Set<StudentCourseEntity>().FirstOrDefaultAsync(x => x.Id == id);
            _context.Set<StudentCourseEntity>().Remove(studentCourse);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
