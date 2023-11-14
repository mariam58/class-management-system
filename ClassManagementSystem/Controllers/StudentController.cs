using ClassManagementSystem.Interfaces;
using ClassManagementSystem.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace ClassManagementSystem.Controllers
{
    [Controller]
    [Route("[Controller]")]
    public class StudentController : Controller
    {
        private readonly IUnitOfWork _uow;
        public StudentController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet("GetStudents")]
        public async Task<IActionResult> GetStudents()
        {
            var data = await _uow.studentRepository.GetAllAsync();
            var settings = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.Indented
            };
            var parsedData = JsonConvert.SerializeObject(data, settings);
            return Ok(parsedData);
        }

        [HttpPost("AddStudent")]
        public async Task<IActionResult> AddStudent(string firstName, string lastName, int age, string email)
        {
           
                var student = new StudentEntity()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Age = age,
                    Email = email
                };

                await _uow.studentRepository.Add(student);
            var settings = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.Indented
            };
            var parsedData = JsonConvert.SerializeObject(student, settings);
            return Ok(parsedData);
           
        }

        [HttpPut("EditStudent")]
        public async Task<IActionResult> EditStudent(string firstName, string lastName, int age, string email, Guid id)
        {
           
                var student = await _uow.studentRepository.GetById(id);
                /*  if (student == null) throw new Exception("null");*/
                student.FirstName = firstName;
                student.LastName = lastName;
                student.Age = age;
                student.Email = email;
                await _uow.studentRepository.Update(student);
            var settings = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.Indented
            };
            var parsedData = JsonConvert.SerializeObject(student, settings);
            return Ok(parsedData);
           
        }

        [HttpDelete("DeleteStudent")]
        public async Task<IActionResult> DeleteStudent(Guid id)
        {
           
                var student = await _uow.studentRepository.GetById(id);
                if (student == null) throw new Exception("null");
                await _uow.studentRepository.Delete(student);           
                return Ok();
           
        }
    }
}
