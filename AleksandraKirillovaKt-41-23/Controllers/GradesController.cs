using AleksandraKirillovaKt_41_23.Filters.GradeFilter;
using AleksandraKirillovaKt_41_23.Filters.StudentFilter;
using AleksandraKirillovaKt_41_23.Interfaces;
using AleksandraKirillovaKt_41_23.Models;
using Microsoft.AspNetCore.Mvc;

namespace AleksandraKirillovaKt_41_23.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GradesController : ControllerBase
    {
        private readonly ILogger<GradesController> _logger;
        private readonly IGradeService _gradeService;

        public GradesController(ILogger<GradesController> logger, IGradeService gradeService)
        {
            _logger = logger;
            _gradeService = gradeService;
        }


        [HttpPost("by_gradestudent", Name = "GetGradesByStudent")]
        public async Task<IActionResult> GetGradesByStudentAsync(GradeStudentFilter filter, CancellationToken cancellationToken = default)
        {
            var grades = await _gradeService.GetGradesByStudentAsync(filter, cancellationToken);
            return Ok(grades);
        }
        [HttpPost("by_average_disciplinegroup", Name = "GetGradesByAverageDisciplineGroup")]
        public async Task<IActionResult> GetGradesByAverageDisciplineGroupAsync(GradeGroupDisciplineFilter filter, CancellationToken cancellationToken = default)
        {
            var grades = await _gradeService.GetGradesByAverageDisciplineGroupAsync(filter, cancellationToken);
            return Ok(grades);
        }
        [HttpPost("by_averagecourse", Name = "GetGradesByAverageCourse")]
        public async Task<IActionResult> GetGradesByAverageCourseAsync(GradeCoursFilter filter, CancellationToken cancellationToken = default)
        {
            var grades = await _gradeService.GetGradesByAverageCourseAsync(filter, cancellationToken);
            return Ok(grades);
        }
        [HttpPost(Name = "CreateGrade")]
        public async Task<IActionResult> CreateGradeAsync(GradeAddUpFilter grade, CancellationToken cancellationToken = default)
        {
            var grades = await _gradeService.CreateGradeAsync(grade, cancellationToken);
            return Ok(grades);
        }
        [HttpPut(Name = "UpDataGrade")]
        public async Task<IActionResult> UpDataGradeAsync(GradeAddUpFilter gradeNew, CancellationToken cancellationToken = default)
        {
            var grades = await _gradeService.UpDataGradeAsync(gradeNew, cancellationToken);
            if (grades == null)
                return NotFound("Оценка не найдена.");
            return Ok(grades);
        }
    }
}
