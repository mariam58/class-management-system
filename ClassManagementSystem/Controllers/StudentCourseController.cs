using ClassManagementSystem.Interfaces;
using ClassManagementSystem.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ClassManagementSystem.Controllers
{
    [Controller]
    [Route("[Controller]")]
    public class StudentCourseController : Controller
    {
        private readonly IUnitOfWork _uow;
        public StudentCourseController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet("GetStudentCourses")]
        public async Task<IActionResult> GetStudentCourses()
        {
            var data = await _uow.studentCourseRepository.GetAllAsync();
            var settings = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.Indented
            };
            var parsedData = JsonConvert.SerializeObject(data, settings);
            return Ok(parsedData);
        }

        [HttpPost("AddStudentCourse")]
        public async Task<IActionResult> AddStudentCourse(Guid studentId, Guid courseId)
        {
           
                var studentCourse = new StudentCourseEntity()
                {
                    StudentId = studentId,
                    CourseId = courseId
                };

                await _uow.studentCourseRepository.Add(studentCourse);
             
                var settings = new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    Formatting = Formatting.Indented
                };
                var parsedData = JsonConvert.SerializeObject(studentCourse, settings);
                return Ok(parsedData);
           
        }

        [HttpDelete("DeleteStudentCourse")]
        public async Task<IActionResult> DeleteStudentCourse(Guid id)
        {
           
                var studentCourse = await _uow.studentCourseRepository.GetById(id);
                if (studentCourse == null) throw new Exception("null");
                await _uow.studentCourseRepository.Delete(studentCourse);
          
                return Ok();
           
        }
    }
}
