using AleksandraKirillovaKt_41_23.Filters.GroupFilter;
using AleksandraKirillovaKt_41_23.Filters.StudentFilter;
using AleksandraKirillovaKt_41_23.Interfaces;
using AleksandraKirillovaKt_41_23.Models;
using Microsoft.AspNetCore.Mvc;

namespace AleksandraKirillovaKt_41_23.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GroupController: ControllerBase
    {
        private readonly ILogger<GroupController> _logger;
        private readonly IGroupService _groupService;

        public GroupController(ILogger<GroupController> logger, IGroupService groupService)
        {
            _logger = logger;
            _groupService = groupService;
        }

        [HttpGet("by_specialtie", Name = "GetGroupsBySpecialtie")]
        public async Task<IActionResult> GetGroupsBySpecialtieAsync([FromQuery] GroupSpecialtieFilter filter, CancellationToken cancellationToken = default)
        {
            var groups = await _groupService.GetGroupsBySpecialtieAsync(filter, cancellationToken);
            return Ok(groups);
        }
        [HttpGet("by_year", Name = "GetGroupsByYear")]
        public async Task<IActionResult> GetGroupsByYearAsync([FromQuery] GroupYearFilter filter, CancellationToken cancellationToken = default)
        {
            var groups = await _groupService.GetGroupsByYearAsync(filter, cancellationToken);
            return Ok(groups);
        }

        [HttpPost(Name = "CreateGroup")]
        public async Task<IActionResult> CreateGroupAsync(GroupAddUpFilter academicGroups, CancellationToken cancellationToken = default)
        {
            var groups = await _groupService.CreateGroupAsync(academicGroups, cancellationToken);
            return Ok(groups);
        }
        [HttpPut(Name = "UpDataGroup")]
        public async Task<IActionResult> UpDataGroupAsync(GroupAddUpFilter academicGroupsNew, CancellationToken cancellationToken = default)
        {
            var groups = await _groupService.UpDataGroupAsync(academicGroupsNew, cancellationToken);
            if(groups== null)
                return NotFound("Группа не найдена.");
            return Ok(groups);
        }
        [HttpDelete(Name = "DeleteGroup")]
        public async Task<IActionResult> DeleteGroupAsync(int idDelete, CancellationToken cancellationToken = default)
        {
            var groupsIsDel = await _groupService.DeleteGroupAsync(idDelete, cancellationToken);
            if (!groupsIsDel)
                return NotFound("Группа не найдена.");
            return Ok("Группа удалена");
        }
    }
}
