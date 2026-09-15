using AleksandraKirillovaKt_41_23.Database.Configurations;
using AleksandraKirillovaKt_41_23.Models;
using Microsoft.EntityFrameworkCore;

namespace AleksandraKirillovaKt_41_23.Database
{
    public class StudentDbContext: DbContext
    {
        //Добавление таблиц

        DbSet<Students> Students { get; set; }
        DbSet<AcademicGroups> AcademicGroups { get; set; }
        DbSet<Specialties> Specialties { get; set; }
        DbSet<Disciplines> Disciplines { get; set; }
        DbSet<Grades> Grades { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Добавление конфигураций к таблицам
            modelBuilder.ApplyConfiguration(new StudentsConfiguration());
            modelBuilder.ApplyConfiguration(new AcademicGroupsConfigurations());
            modelBuilder.ApplyConfiguration(new SpecialtiesConfiguration());
            modelBuilder.ApplyConfiguration(new DisciplinesConfiguration());
            modelBuilder.ApplyConfiguration(new GradesConfiguration());
        }

        public StudentDbContext(DbContextOptions <StudentDbContext> options) : base(options)
        {

        }
    }
}
