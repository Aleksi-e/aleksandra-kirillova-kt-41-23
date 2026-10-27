using AleksandraKirillovaKt_41_23.Database;
using AleksandraKirillovaKt_41_23.Filters.DisciplineFilter;
using AleksandraKirillovaKt_41_23.Filters.GroupFilter;
using AleksandraKirillovaKt_41_23.Models;
using Microsoft.EntityFrameworkCore;


namespace AleksandraKirillovaKt_41_23.Interfaces
{
    public interface IDisciplineService
    {
        Task<Disciplines[]> GetDisciplineByDirectionAsync(DisciplineDirectionFilter filter, CancellationToken cancellationToken);
        Task<Disciplines> CreateDisciplineAsync(Disciplines discipline, CancellationToken cancellationToken);
        Task<Disciplines> UpDataDisciplineAsync(Disciplines disciplineNew, CancellationToken cancellationToken);
        Task<bool> DeleteDisciplineAsync(int idDelete, CancellationToken cancellationToken);
    }
    public class DisciplineService : IDisciplineService
    {
        private readonly StudentDbContext _dbContext;

        public DisciplineService(StudentDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Disciplines[]> GetDisciplineByDirectionAsync(DisciplineDirectionFilter filter, CancellationToken cancellationToken = default)
        {
            var disciplines = await _dbContext.Set<Disciplines>()
                .Where(e => e.DisciplineDirection == filter.DisciplineDirection && !e.StatusDeleted)
                .Select(s => new Disciplines
                {
                    DisciplineId = s.DisciplineId,
                    DisciplineName = s.DisciplineName
                })
                .ToArrayAsync(cancellationToken);
            return disciplines;
        }


        //Создание
        public async Task<Disciplines> CreateDisciplineAsync(Disciplines discipline, CancellationToken cancellationToken = default)
        {
            var disciplineNew = new Disciplines
            {
                DisciplineName = discipline.DisciplineName,
                DisciplineDirection = discipline.DisciplineDirection,
                StatusDeleted = false,

            };
            _dbContext.Set<Disciplines>().Add(disciplineNew);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return disciplineNew;
        }
        //Изменение
        public async Task<Disciplines> UpDataDisciplineAsync(Disciplines disciplineNew, CancellationToken cancellationToken = default)
        {
            var discipline = await _dbContext.Set<Disciplines>()
                .FirstOrDefaultAsync(e => e.DisciplineId == disciplineNew.DisciplineId && !e.StatusDeleted);
            if (discipline == null)
            {
                return null;
            }
            if (disciplineNew.DisciplineName != null && disciplineNew.DisciplineName != "string")
                discipline.DisciplineName = disciplineNew.DisciplineName;
            if (disciplineNew.DisciplineDirection != null && disciplineNew.DisciplineDirection != "string")
                discipline.DisciplineDirection = disciplineNew.DisciplineDirection;


            await _dbContext.SaveChangesAsync(cancellationToken);
            return discipline;
        }
        //Удаление
        public async Task<bool> DeleteDisciplineAsync(int idDelete, CancellationToken cancellationToken = default)
        {
            var discipline = await _dbContext.Set<Disciplines>()
                .FirstOrDefaultAsync(e => e.DisciplineId == idDelete && !e.StatusDeleted);
            if (discipline == null)
            {
                return false;
            }
            discipline.StatusDeleted = true;

            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
