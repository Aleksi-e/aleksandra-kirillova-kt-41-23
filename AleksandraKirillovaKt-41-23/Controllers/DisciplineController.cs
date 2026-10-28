using AleksandraKirillovaKt_41_23.Filters.DisciplineFilter;
using AleksandraKirillovaKt_41_23.Filters.GroupFilter;
using AleksandraKirillovaKt_41_23.Interfaces;
using AleksandraKirillovaKt_41_23.Models;
using Microsoft.AspNetCore.Mvc;

namespace AleksandraKirillovaKt_41_23.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DisciplineController: ControllerBase
    {
        private readonly ILogger<DisciplineController> _logger;
        private readonly IDisciplineService _disciplineService;

        public DisciplineController(ILogger<DisciplineController> logger, IDisciplineService disciplineService)
        {
            _logger = logger;
            _disciplineService = disciplineService;
        }
        [HttpGet("by_direction", Name = "GetDisciplineByDirection")]
        public async Task<IActionResult> GetDisciplineByDirectionAsync([FromQuery] DisciplineDirectionFilter filter, CancellationToken cancellationToken = default)
        {
            var disciplines = await _disciplineService.GetDisciplineByDirectionAsync(filter, cancellationToken);
            return Ok(disciplines);
        }

        [HttpPost(Name = "CreateDiscipline")]
        public async Task<IActionResult> CreateDisciplineAsync(Disciplines discipline, CancellationToken cancellationToken = default)
        {
            var disciplines = await _disciplineService.CreateDisciplineAsync(discipline, cancellationToken);
            return Ok(disciplines);
        }
        [HttpPut(Name = "UpDataDiscipline")]
        public async Task<IActionResult> UpDataDisciplineAsync(Disciplines disciplineNew, CancellationToken cancellationToken = default)
        {
            var disciplines = await _disciplineService.UpDataDisciplineAsync(disciplineNew, cancellationToken);
            if (disciplines == null)
                return NotFound("Дисциплина не найдена.");
            return Ok(disciplines);
        }
        [HttpDelete(Name = "DeleteDiscipline")]
        public async Task<IActionResult> DeleteDisciplineAsync(int idDelete, CancellationToken cancellationToken = default)
        {
            var disciplinesIsDel = await _disciplineService.DeleteDisciplineAsync(idDelete, cancellationToken);
            if (!disciplinesIsDel)
                return NotFound("Дисциплина не найдена.");
            return Ok("Дисциплина удалена.");
        }
    }
}
