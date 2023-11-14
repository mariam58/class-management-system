using ClassManagementSystem.Interfaces;
using ClassManagementSystem.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ClassManagementSystem.Controllers
{
    [Controller]
    [Route("[Controller]")]
    public class TeacherController : Controller
    {
        private readonly IUnitOfWork _uow;
        public TeacherController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet("GetTeacher")]
        public async Task<IActionResult> GetTeacher()
        {
            var data = await _uow.teacherRepository.GetAllAsync();
            var settings = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.Indented
            };
            var parsedData = JsonConvert.SerializeObject(data, settings);
            return Ok(parsedData);
        }

        [HttpPost("AddTeacher")]
        public async Task<IActionResult> AddTeacher(string firstName, string lastName, int experince)
        {
           
                var teacher = new TeacherEntity()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Experience = experince
                };

                await _uow.teacherRepository.Add(teacher);
            var settings = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.Indented
            };
            var parsedData = JsonConvert.SerializeObject(teacher, settings);
            return Ok(parsedData);
           
        }


        [HttpPut("EditTeacher")]
        public async Task<IActionResult> EditTeacher(string firstName, string lastName, int experince, Guid id)
        {
            
                var teacher = await _uow.teacherRepository.GetById(id);
                /*  if (student == null) throw new Exception("null");*/
                teacher.FirstName = firstName;
                teacher.LastName = lastName;
                teacher.Experience = experince;
                await _uow.teacherRepository.Update(teacher);

            var settings = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.Indented
            };
            var parsedData = JsonConvert.SerializeObject(teacher, settings);
            return Ok(parsedData);

        }

        [HttpDelete("DeleteTeacher")]
        public async Task<IActionResult> DeleteTeacher(Guid id)
        {
            var teacher = await _uow.teacherRepository.GetById(id);
            if (teacher == null) throw new Exception("null");
            await _uow.teacherRepository.Delete(teacher);
            return Ok();
        }
    }
}
