using FinalExam.Entities;
using FinalExam.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FinalExam.Controllers
{
    [Controller]
    [Route("[Controller]")]
    public class TeacherController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly FinalExamDbContext _context;
        public TeacherController(IUnitOfWork uow, FinalExamDbContext context)
        {
            _uow = uow;
            _context = context;
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
            try
            {
                await _context.Database.BeginTransactionAsync();
                var teacher = new TeacherEntity()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Experience = experince
                };

                await _uow.teacherRepository.Add(teacher);
                await _context.Database.CommitTransactionAsync();
                return Ok(teacher);
            }
            catch (Exception ex)
            {
                await _context.Database.RollbackTransactionAsync();
                throw ex;
            }
        }


        [HttpPut("EditTeacher")]
        public async Task<IActionResult> EditTeacher(string firstName, string lastName, int experince, Guid id)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();
                var teacher = await _uow.teacherRepository.GetById(id);
                /*  if (student == null) throw new Exception("null");*/
                teacher.FirstName = firstName;
                teacher.LastName = lastName;
                teacher.Experience = experince;
                await _uow.teacherRepository.Update(teacher);
                await _context.Database.CommitTransactionAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                await _context.Database.RollbackTransactionAsync();
                throw ex;
            }
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
