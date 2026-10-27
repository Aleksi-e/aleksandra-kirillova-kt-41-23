using AleksandraKirillovaKt_41_23.Database;
using AleksandraKirillovaKt_41_23.Filters.GradeFilter;
using AleksandraKirillovaKt_41_23.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NLog.Targets;
using System.Linq;

namespace AleksandraKirillovaKt_41_23.Interfaces
{
    public interface IGradeService
    {
        Task<Grades[]> GetGradesByStudentAsync(GradeStudentFilter filter, CancellationToken cancellationToken);
        Task<double> GetGradesByAverageDisciplineGroupAsync(GradeGroupDisciplineFilter filter, CancellationToken cancellationToken = default);
        Task<double> GetGradesByAverageCourseAsync(GradeCoursFilter filter, CancellationToken cancellationToken = default);
        Task<Grades> CreateGradeAsync(GradeAddUpFilter grade, CancellationToken cancellationToken);
        Task<Grades> UpDataGradeAsync(GradeAddUpFilter gradeNew, CancellationToken cancellationToken);
    }

    public class GradeService : IGradeService
    {
        private readonly StudentDbContext _dbContext;

        public GradeService(StudentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Grades[]> GetGradesByStudentAsync(GradeStudentFilter filter, CancellationToken cancellationToken = default)
        {
            var grades = await _dbContext.Set<Grades>()
               .Where(e => !e.Students!.StatusDeleted)
               .Where(e => e.Students!.LastName == filter.LastName && e.Students!.FirstName == filter.FirstName)
               .Select(s => new Grades
               {
                   StudentId = s.StudentId,
                   DisciplineId = s.DisciplineId,
                   Score = s.Score,
                   Students = null,
                   Disciplines = new Disciplines
                   {
                       DisciplineName = s.Disciplines!.DisciplineName
                   }

               })
               .ToArrayAsync(cancellationToken);
            return grades;
        }
        public async Task<double> GetGradesByAverageDisciplineGroupAsync(GradeGroupDisciplineFilter filter, CancellationToken cancellationToken = default)
        {
            var averageGroup = await _dbContext.Set<Grades>()
              .Where(e => !e.Students!.StatusDeleted)
              .Where(e =>  e.Students!.AcademicGroups!.AcademicGroupName == filter.AcademicGroupName && e.Disciplines!.DisciplineName == filter.DisciplineName)
              .AverageAsync(s => (double?)s.Score, cancellationToken) ?? 0;
            return averageGroup;
        }
        public async Task<double> GetGradesByAverageCourseAsync(GradeCoursFilter filter, CancellationToken cancellationToken = default) //cancellationToken - механизм для отмены долгих операций
        {
            var averageCourse = await _dbContext.Set<Grades>()
              .Where(e => e.Students!.AcademicGroups!.Course == filter.Course && !e.Students!.StatusDeleted)
              .AverageAsync(s => (double?)s.Score, cancellationToken) ?? 0;
            return averageCourse;
        }

        //Создание
        public async Task<Grades> CreateGradeAsync(GradeAddUpFilter grade, CancellationToken cancellationToken = default)
        {
            var gradeNew = new Grades
            {
                Score = grade.Score,
                GradeDate = grade.GradeDate,
                StudentId = grade.StudentId,
                DisciplineId =grade.DisciplineId
            };
            _dbContext.Set<Grades>().Add(gradeNew);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return gradeNew;
        }
        //Изменение
        public async Task<Grades> UpDataGradeAsync(GradeAddUpFilter gradeNew, CancellationToken cancellationToken = default)
        {
            var grade = await _dbContext.Set<Grades>()
                .FirstOrDefaultAsync(e => e.GradeId == gradeNew.GradeId && e.StudentId == gradeNew.StudentId && !e.Students!.StatusDeleted);
            if (grade == null)
            {
                return null;
            }
            if (gradeNew.Score != 0)
                grade.Score = gradeNew.Score;
            if (gradeNew.GradeDate != default)
                grade.GradeDate = gradeNew!.GradeDate;
            if (gradeNew.DisciplineId != 0)
                grade.DisciplineId = gradeNew.DisciplineId;
            grade.Students = null;
            grade.Disciplines = null;


            await _dbContext.SaveChangesAsync(cancellationToken);
            return grade;
        }
    }
}
