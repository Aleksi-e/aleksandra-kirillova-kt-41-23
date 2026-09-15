using AleksandraKirillovaKt_41_23.Database.Helpers;
using AleksandraKirillovaKt_41_23.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AleksandraKirillovaKt_41_23.Database.Configurations
{
    public class GradesConfiguration : IEntityTypeConfiguration<Grades>
    {
        private const string TableName = "cd_grades";
        public void Configure(EntityTypeBuilder<Grades> builder)
        {
            builder.ToTable(TableName);

            builder.HasKey(p => p.GradeId)
                .HasName($"pk_{TableName}_grades_id");

            builder.Property(p => p.GradeId)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.GradeId)
                .HasColumnName("grades_id")
                .HasComment("Идентификатор оценок");

            builder.Property(p => p.Score)
                .IsRequired()
                .HasColumnName("n_score")
                .HasColumnType(ColumnType.Int)
                .HasComment("Оценка");

            builder.Property(p => p.GradeDate)
                .IsRequired()
                .HasColumnName("d_gradesdate")
                .HasColumnType(ColumnType.DateTime)
                .HasComment("Дата получения оценки");

            //Связь со студентом
            builder.HasOne(p => p.Students)
                .WithMany()
                .HasForeignKey(p => p.StudentId)
                .HasConstraintName("fk_f_grades_students_id")
                .OnDelete(DeleteBehavior.Cascade); //При удалении студента, его оценки удаляются

            //Связь с дисциплиной
            builder.HasOne(p => p.Disciplines)
                .WithMany()
                .HasForeignKey(p => p.DisciplineId)
                .HasConstraintName("fk_f_grades_disciplines_id")
                .OnDelete(DeleteBehavior.Restrict); //При удалении дисциплины

            builder.HasIndex(p => p.StudentId, $"idx_{TableName}_fk_f_students_id");
            builder.HasIndex(p => p.DisciplineId, $"idx_{TableName}_fk_f_disciplines_id");

            //Автозагрузка студента при запросе оценки
            builder.Navigation(p => p.Students)
                .AutoInclude();
            //Автозагрузка дисциплины при запросе оценки
            builder.Navigation(p => p.Disciplines)
                .AutoInclude();
        }
    }
}
