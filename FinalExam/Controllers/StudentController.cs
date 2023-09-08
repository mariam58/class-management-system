using FinalExam.Entities;
using FinalExam.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinalExam.Controllers
{
    [Controller]
    [Route("[Controller]")]
    public class StudentController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly FinalExamDbContext _context;
        public StudentController(IUnitOfWork uow, FinalExamDbContext context)
        {
            _uow = uow;
            _context = context;
        }

        [HttpGet("GetStudents")]
        public async Task<IActionResult> GetStudents()
        {
            var data = await _uow.studentRepository.GetAllAsync();
            return Ok(data);
        }

        [HttpPost("AddStudent")]
        public async Task<IActionResult> AddStudent(string firstName, string lastName, int age, string email)
        {
            try 
            {
                await _context.Database.BeginTransactionAsync();
                var student = new StudentEntity()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Age = age,
                    Email = email
                };

                await _uow.studentRepository.Add(student);
                await _context.Database.CommitTransactionAsync();
                return Ok(student);
            }
            catch (Exception ex)
            {
                await _context.Database.RollbackTransactionAsync();
                throw ex;
            }  
        }

        [HttpPut("EditStudent")]
        public async Task<IActionResult> EditStudent(string firstName, string lastName, int age, string email, Guid id)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();
                var student = await _uow.studentRepository.GetById(id);
              /*  if (student == null) throw new Exception("null");*/
                student.FirstName = firstName;
                student.LastName = lastName;
                student.Age = age;
                student.Email = email;
                await _uow.studentRepository.Update(student);
                await _context.Database.CommitTransactionAsync();
                return Ok();
            }
            catch(Exception ex) 
            {
                await _context.Database.RollbackTransactionAsync();
                throw ex;
            } 
        }

        [HttpDelete("DeleteStudent")]
        public async Task<IActionResult> DeleteStudent(Guid id)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();
                var student = await _uow.studentRepository.GetById(id);
                if (student == null) throw new Exception("null");
                await _uow.studentRepository.Delete(student);
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
