using AleksandraKirillovaKt_41_23.Filters.StudentFilter;
using AleksandraKirillovaKt_41_23.Interfaces;
using AleksandraKirillovaKt_41_23.Models;
using Microsoft.AspNetCore.Mvc;

namespace AleksandraKirillovaKt_41_23.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly ILogger<StudentsController> _logger;
        private readonly IStudentService _studentService;

        public StudentsController(ILogger<StudentsController> logger, IStudentService studentService)
        {
            _logger = logger;
            _studentService = studentService;
        }

        [HttpGet("by_groupname", Name = "GetStudentsByGroup")]
        public async Task<IActionResult> GetStudentsByGroupAsync([FromQuery] StudentGroupFilter filter, CancellationToken cancellationToken = default)
        {
            var students = await _studentService.GetStudentsByGroupAsync(filter, cancellationToken);
            return Ok(students);
        }
        [HttpGet("by_lastname", Name = "GetStudentsByName")]
        public async Task<IActionResult> GetStudentsByNameAsync([FromQuery] StudentNameFilter filter, CancellationToken cancellationToken = default)
        {
            var students = await _studentService.GetStudentsByNameAsync(filter, cancellationToken);
            return Ok(students);
        }

        [HttpGet("by_dop", Name = "GetDop")]
        public async Task<IActionResult> GetDopAsunce([FromQuery] StudentGroupFilter filterDop, CancellationToken cancellationToken = default)
        {
            var students = await _studentService.GetDopAsunce(filterDop, cancellationToken);
            return Ok(students);
        }


        [HttpPost(Name = "CreateStudent")]
        public async Task<IActionResult> CreateStudentAsync(StudentAddUpFilter student, CancellationToken cancellationToken = default)
        {
            var students = await _studentService.CreateStudentAsync(student, cancellationToken);
            return Ok(students);
        }
        [HttpPut(Name = "UpDataStudent")]
        public async Task<IActionResult> UpDataStudentAsync(StudentAddUpFilter studentNew, CancellationToken cancellationToken = default)
        {
            var students = await _studentService.UpDataStudentAsync(studentNew, cancellationToken);
            if (students == null)
                return NotFound("Студент не найден.");
            return Ok(students);
        }
        [HttpDelete(Name = "DeleteStudent")]
        public async Task<IActionResult> DeleteStudentAsync(int idDelete, CancellationToken cancellationToken = default)
        {
            var studentsIsDel = await _studentService.DeleteStudentAsync(idDelete, cancellationToken);
            if (!studentsIsDel)
                return NotFound("Студент не найден.");
            return Ok("Студент удален.");
        }
    }
}
