using AleksandraKirillovaKt_41_23.Database.Helpers;
using AleksandraKirillovaKt_41_23.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AleksandraKirillovaKt_41_23.Database.Configurations
{
    public class StudentsConfiguration : IEntityTypeConfiguration<Students>
    {
        private const string TableName = "cd_student";
        public void Configure(EntityTypeBuilder<Students> builder)
        {
            builder.HasKey(p => p.StudentId)
                .HasName($"pk_{TableName}_student_id");

            builder.Property(p => p.StudentId)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.StudentId)
                .HasColumnName("student_id")
                .HasComment("Идентификатор студента");

            builder.Property(p => p.FirstName)
                .IsRequired()
                .HasColumnName("c_student_firstname")
                .HasColumnType(ColumnType.String50).HasMaxLength(50)
                .HasComment("Имя студента");

            builder.Property(p => p.LastName)
                .IsRequired()
                .HasColumnName("c_student_lastname")
                .HasColumnType(ColumnType.String50).HasMaxLength(50)
                .HasComment("Фамилия студента");

            builder.Property(p => p.BirthDate)
                .IsRequired()
                .HasColumnName("d_birthdate")
                .HasColumnType(ColumnType.DateTime)
                .HasComment("Дата рождения студента");

            builder.Property(p => p.StatusDeleted)
                .IsRequired()
                .HasColumnName("b_statusdeleted")
                .HasColumnType(ColumnType.Bool)
                .HasDefaultValue(false)
                .HasComment("Статус удаления");


            //Связь с академической группой
            builder.HasOne(p => p.AcademicGroups)
                .WithMany()
                .HasForeignKey(p => p.AcademicGroupId)
                .HasConstraintName("fk_f_academic_groups_id")
                .OnDelete(DeleteBehavior.Cascade); //если будет удалена группа, то и студенты в ней

            builder.ToTable(TableName)
                .HasIndex(p => p.AcademicGroupId, $"idx_{TableName}_fk_f_academic_groups_id");

            builder.HasIndex(p => p.LastName, $"idx_{TableName}_lastname");
            builder.HasIndex(p => new { p.LastName, p.FirstName }, $"idx_{TableName}_polnoename");

            //Автозагрузка группы при запросе студента
            builder.Navigation(p => p.AcademicGroups)
                .AutoInclude();

        }
    }
}
