using BLL.DTOModels.RequestsDTO;
using BLL.DTOModels.ResponseDTO;
using BLL.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PU_KOL1.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        // GET: api/student
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentResponseDTO>>> GetAllStudents()
        {
            var students = await _studentService.GetAllStudentsAsync();
            return Ok(students);
        }

        // GET: api/student/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentResponseDTO>> GetStudentById(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        // POST: api/student
        [HttpPost]
        public async Task<ActionResult<StudentResponseDTO>> CreateStudent(StudentRequestDTO studentRequestDTO)
        {
            var student = await _studentService.CreateStudentAsync(studentRequestDTO);
            return CreatedAtAction(nameof(GetStudentById), new { id = student.ID }, student);
        }

        // PUT: api/student/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, StudentRequestDTO studentRequestDTO)
        {
            var student = await _studentService.UpdateStudentAsync(id, studentRequestDTO);
            if (student == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        // DELETE: api/student/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var deleted = await _studentService.DeleteStudentAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
