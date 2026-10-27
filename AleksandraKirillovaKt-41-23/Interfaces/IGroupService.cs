using AleksandraKirillovaKt_41_23.Database;
using AleksandraKirillovaKt_41_23.Filters.GroupFilter;
using AleksandraKirillovaKt_41_23.Filters.StudentFilter;
using AleksandraKirillovaKt_41_23.Models;
using Microsoft.EntityFrameworkCore;

namespace AleksandraKirillovaKt_41_23.Interfaces
{
    public interface IGroupService
    {
        Task<AcademicGroups[]> GetGroupsBySpecialtieAsync(GroupSpecialtieFilter filter, CancellationToken cancellationToken);
        Task<AcademicGroups[]> GetGroupsByYearAsync(GroupYearFilter filter, CancellationToken cancellationToken);
        Task<AcademicGroups> CreateGroupAsync(GroupAddUpFilter academicGroups, CancellationToken cancellationToken);
        Task<AcademicGroups> UpDataGroupAsync(GroupAddUpFilter academicGroupsNew, CancellationToken cancellationToken);
        Task<bool> DeleteGroupAsync(int idDelete, CancellationToken cancellationToken);
    }
    public class GroupService : IGroupService
    {
        private readonly StudentDbContext _dbContext;

        public GroupService(StudentDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<AcademicGroups[]> GetGroupsBySpecialtieAsync(GroupSpecialtieFilter filter, CancellationToken cancellationToken = default)
        {
            var groups = await _dbContext.Set<AcademicGroups>()
                .Where(e => e.Specialties!.SpecialtieName == filter.SpecialtieName && !e.StatusDeleted)
                .Select(s => new AcademicGroups
                {
                    AcademicGroupId = s.AcademicGroupId,
                    Course = s.Course,
                    AcademicGroupName = s.AcademicGroupName,
                    SpecialtieId = s.SpecialtieId,
                    Specialties = null
                })
                .ToArrayAsync(cancellationToken);
            return groups;
        }
        public async Task<AcademicGroups[]> GetGroupsByYearAsync(GroupYearFilter filter, CancellationToken cancellationToken = default)
        {
            var groups = await _dbContext.Set<AcademicGroups>()
                .Where(e => e.Course == filter.Course && !e.StatusDeleted)
                .Select(s => new AcademicGroups
                {
                    AcademicGroupId = s.AcademicGroupId,
                    Course = s.Course,
                    AcademicGroupName = s.AcademicGroupName,
                    SpecialtieId = s.SpecialtieId,
                    Specialties = null
                })
                .ToArrayAsync(cancellationToken);
            return groups;
        }


        //Создание
        public async Task<AcademicGroups> CreateGroupAsync(GroupAddUpFilter academicGroups, CancellationToken cancellationToken = default)
        {
            var groupNew = new AcademicGroups
            {
                Course = academicGroups.Course,
                AcademicGroupName = academicGroups.AcademicGroupName,
                StatusDeleted = false,
                SpecialtieId = academicGroups.SpecialtieId            
            };
            _dbContext.Set<AcademicGroups>().Add(groupNew);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return groupNew;
        }
        //Изменение
        public async Task<AcademicGroups> UpDataGroupAsync(GroupAddUpFilter academicGroupsNew, CancellationToken cancellationToken = default)
        {
            var group = await _dbContext.Set<AcademicGroups>()
                .FirstOrDefaultAsync(e => e.AcademicGroupId == academicGroupsNew.AcademicGroupId && !e.StatusDeleted);
            if (group == null) {
                return null;
            }
            if(academicGroupsNew.AcademicGroupName != null && academicGroupsNew.AcademicGroupName !="string") 
                group.AcademicGroupName=academicGroupsNew.AcademicGroupName;
            if(academicGroupsNew.Course != 0)
                group.Course = academicGroupsNew!.Course;
            if (academicGroupsNew.SpecialtieId != 0)
                group.SpecialtieId = academicGroupsNew.SpecialtieId;
            group.Specialties = null;


            await _dbContext.SaveChangesAsync(cancellationToken);
            return group;
        }
        //Удаление
        public async Task<bool> DeleteGroupAsync(int idDelete, CancellationToken cancellationToken = default)
        {
            var group = await _dbContext.Set<AcademicGroups>()
                .FirstOrDefaultAsync(e => e.AcademicGroupId == idDelete && !e.StatusDeleted);
            if (group == null)
            {
                return false;
            }
            group.StatusDeleted = true;

            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
