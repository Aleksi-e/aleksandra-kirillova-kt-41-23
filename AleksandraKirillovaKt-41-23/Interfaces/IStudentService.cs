using AleksandraKirillovaKt_41_23.Database;
using AleksandraKirillovaKt_41_23.Filters.StudentFilter;
using AleksandraKirillovaKt_41_23.Models;
using Microsoft.EntityFrameworkCore;

namespace AleksandraKirillovaKt_41_23.Interfaces
{
    public interface IStudentService
    {
        Task<Students[]> GetStudentsByGroupAsync(StudentGroupFilter filter, CancellationToken cancellationToken);
        Task<Students[]> GetStudentsByNameAsync(StudentNameFilter filter, CancellationToken cancellationToken);
        Task<Students> CreateStudentAsync(StudentAddUpFilter student, CancellationToken cancellationToken);
        Task<Students> UpDataStudentAsync(StudentAddUpFilter studentNew, CancellationToken cancellationToken);
        Task<bool> DeleteStudentAsync(int idDelete, CancellationToken cancellationToken);
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
                .Where(e=>e.AcademicGroups!.AcademicGroupName == filter.AcademicGroupName && !e.StatusDeleted) 
                .Select(s=> new Students
                {
                    StudentId=s.StudentId,
                    FirstName=s.FirstName,
                    LastName=s.LastName,
                    BirthDate = s.BirthDate,
                    AcademicGroupId = s.AcademicGroupId,
                    AcademicGroups =null
                })
                .ToArrayAsync(cancellationToken);
            return students;
        }
        public async Task<Students[]> GetStudentsByNameAsync(StudentNameFilter filter, CancellationToken cancellationToken = default)
        {
            var students = await _dbContext.Set<Students>()
                .Where(e => !e.StatusDeleted)
                .Where(e=>(filter.LastName == "" || e.LastName == filter.LastName ) && (filter.FirstName == "" || e.FirstName == filter.FirstName))
                .Select(s => new Students
                {
                    StudentId = s.StudentId,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    BirthDate = s.BirthDate,
                    AcademicGroupId = s.AcademicGroupId,
                    AcademicGroups = null
                })
                .ToArrayAsync(cancellationToken);
            return students;
        }


        //Создание
        public async Task<Students> CreateStudentAsync(StudentAddUpFilter student, CancellationToken cancellationToken = default)
        {
            var studentNew = new Students
            {
                FirstName= student.FirstName,
                LastName= student.LastName,
                BirthDate= student.BirthDate,
                AcademicGroupId= student.AcademicGroupId,
                StatusDeleted = false,
                AcademicGroups = null
                
            };
            _dbContext.Set<Students>().Add(studentNew);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return studentNew;
        }
        //Изменение
        public async Task<Students> UpDataStudentAsync(StudentAddUpFilter studentNew, CancellationToken cancellationToken = default)
        {
            var student = await _dbContext.Set<Students>()
                .FirstOrDefaultAsync(e => e.StudentId == studentNew.StudentId && !e.StatusDeleted);
            if (student == null)
            {
                return null;
            }
            if (studentNew.FirstName != null && studentNew.FirstName != "string")
                student.FirstName = studentNew.FirstName;
            if (studentNew.LastName != null && studentNew.LastName != "string")
                student.LastName = studentNew.LastName;
            if(studentNew.BirthDate != default)
                student.BirthDate = studentNew!.BirthDate;
            if (studentNew.AcademicGroupId != 0)
                student.AcademicGroupId = studentNew.AcademicGroupId;
            student.AcademicGroups = null;


            await _dbContext.SaveChangesAsync(cancellationToken);
            return student;
        }
        //Удаление
        public async Task<bool> DeleteStudentAsync(int idDelete, CancellationToken cancellationToken = default)
        {
            var student = await _dbContext.Set<Students>()
                .FirstOrDefaultAsync(e => e.StudentId == idDelete && !e.StatusDeleted);
            if (student == null)
            {
                return false;
            }
            student.StatusDeleted = true;

            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
