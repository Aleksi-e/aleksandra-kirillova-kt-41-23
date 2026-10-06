using AleksandraKirillovaKt_41_23.Database;
using AleksandraKirillovaKt_41_23.Filters;
using AleksandraKirillovaKt_41_23.Models;
using Microsoft.EntityFrameworkCore;

namespace AleksandraKirillovaKt_41_23.Interfaces
{
    public interface IStudentService
    {
        Task<Students[]> GetStudentsByGroupAsync(StudentGroupFilter filter, CancellationToken cancellationToken);
    }
    public class StudentService : IStudentService
    {
        private readonly StudentDbContext _dbContext;

        public StudentService(StudentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Students[]> GetStudentsByGroupAsync(StudentGroupFilter filter, CancellationToken cancellationToken = default)
        {
            var students = await _dbContext.Set<Students>()
                .Where(s => s.AcademicGroups!.AcademicGroupName == filter.AcademicGroupName)
                .ToArrayAsync(cancellationToken);

            return students;
        }
    }
}
