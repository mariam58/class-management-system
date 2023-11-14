using ClassManagementSystem.Interfaces;
using ClassManagementSystem.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ClassManagementSystem.Controllers
{
    [Controller]
    [Route("[Controller]")]
    public class CourseController : Controller
    {
        private readonly IUnitOfWork _uow;
        public CourseController(IUnitOfWork uow)
        {
            _uow = uow;
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
                var course = new CourseEntity()
                {
                    Name = name,
                    Price = price,
                    Description = description,
                    Quantity = quantity,
                    TeacherId = teacherId
                };

                await _uow.courseRepository.Add(course);
                var settings = new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    Formatting = Formatting.Indented
                };
                var parsedData = JsonConvert.SerializeObject(course, settings);
                return Ok(parsedData);
        }

        [HttpPut("EditCourse")]
        public async Task<IActionResult> EditCourse(string name, int price, string description, int quantity, Guid id)
        {   
            var course = await _uow.courseRepository.GetById(id);
            /*   if (course == null) throw new Exception("null");*/
            course.Name = name;
            course.Price = price;
            course.Description = description;
            course.Quantity = quantity;
            await _uow.courseRepository.Update(course);
            var settings = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.Indented
            };
            var parsedData = JsonConvert.SerializeObject(course, settings);
            return Ok(parsedData);
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
