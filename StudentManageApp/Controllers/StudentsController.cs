using Microsoft.AspNetCore.Mvc;
using StudentManageApp.Data;
using StudentManageApp.Models;
// using System.Collections.Generic;
// using System.Threading.Tasks;

namespace StudentManageApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly StudentRepository _repository;

        public StudentsController(StudentRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<Student>> GetStudents()
        {
            return await _repository.GetStudentsAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var student = await _repository.GetStudentAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            return student;
        }

        [HttpPost]
        public async Task<ActionResult<Student>> AddStudent(Student student)
        {
            await _repository.AddStudentAsync(student);
            return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, student);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, Student student)
        {
            if (id != student.Id)
            {
                return BadRequest();
            }

            await _repository.UpdateStudentAsync(student);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            await _repository.DeleteStudentAsync(id);
            return NoContent();
        }
    }
}