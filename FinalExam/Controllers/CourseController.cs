using FinalExam.Entities;
using FinalExam.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FinalExam.Controllers
{
    [Controller]
    [Route("[Controller]")]
    public class CourseController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly FinalExamDbContext _context;
        public CourseController(IUnitOfWork uow, FinalExamDbContext context)
        {
            _uow = uow;
            _context = context;
        }


        [HttpGet("GetCourses")]
        public async Task<IActionResult> GetCourses()
        {
            var data = await _uow.courseRepository.GetAllAsync();

            var settings = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.Indented
            };
            var parsedData = JsonConvert.SerializeObject(data, settings);
            return Ok(parsedData);
        }

        [HttpPost("AddCourse")]
        public async Task<IActionResult> AddCourse(string name, int price, string description, int quantity, Guid teacherId)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();
                var course = new CourseEntity()
                {
                    Name = name,
                    Price = price,
                    Description = description,
                    Quantity = quantity,
                    TeacherId = teacherId
                };

                await _uow.courseRepository.Add(course);
                await _context.Database.CommitTransactionAsync();
                return Ok(course);
            }
            catch (Exception ex)
            {
                await _context.Database.RollbackTransactionAsync();
                throw ex;
            }
        }

        [HttpPut("EditCourse")]
        public async Task<IActionResult> EditCourse(string name, int price, string description, int quantity, Guid id)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();
                var course = await _uow.courseRepository.GetById(id);
             /*   if (course == null) throw new Exception("null");*/
                course.Name = name;
                course.Price = price;
                course.Description = description;
                course.Quantity = quantity;
                await _uow.courseRepository.Update(course);
                await _context.Database.CommitTransactionAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                await _context.Database.RollbackTransactionAsync();
                throw ex;
            }
        }

        [HttpDelete("DeleteCourse")]
        public async Task<IActionResult> DeleteCourse(Guid id)
        {
            var course = await _uow.courseRepository.GetById(id);
            if (course == null) throw new Exception("null");
            await _uow.courseRepository.Delete(course);
            return Ok();
        }

    }
}
