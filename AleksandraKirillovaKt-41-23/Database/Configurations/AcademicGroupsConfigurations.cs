using AleksandraKirillovaKt_41_23.Database.Helpers;
using AleksandraKirillovaKt_41_23.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AleksandraKirillovaKt_41_23.Database.Configurations
{
    public class AcademicGroupsConfigurations : IEntityTypeConfiguration<AcademicGroups>
    {
        private const string TableName = "cd_academic_group";

        public void Configure(EntityTypeBuilder<AcademicGroups> builder)
        {

            //Первичный ключ
            builder.HasKey(p => p.AcademicGroupId)
                .HasName($"pk_{TableName}_academic_group_id");

            //Автогенерация для первичного ключа
            builder.Property(p => p.AcademicGroupId)
                .ValueGeneratedOnAdd();

            //Настройка полей
            builder.Property(p => p.AcademicGroupId)
                .HasColumnName("academic_group_id")
                .HasComment("Идентификатор академической группы");


            builder.Property(p => p.Course)
                .IsRequired()
                .HasColumnName("n_course")
                .HasColumnType(ColumnType.Int)
                .HasComment("Курс обучения");

            builder.Property(p => p.AcademicGroupName)
                .IsRequired()
                .HasColumnName("c_academic_groupname")
                .HasColumnType(ColumnType.String50).HasMaxLength(50)
                .HasComment("Наименование группы");

            builder.Property(p => p.StatusDeleted)
                .IsRequired()
                .HasColumnName("b_statusdeleted")
                .HasColumnType(ColumnType.Bool)
                .HasDefaultValue(false)
                .HasComment("Статус удаления");

            //Связь с специальностью
            builder.HasOne(p => p.Specialties)
                .WithMany()
                .HasForeignKey(p => p.SpecialtieId)
                .HasConstraintName("fk_f_specialtie_id")
                .OnDelete(DeleteBehavior.Restrict); //

            builder.ToTable(TableName)
                .HasIndex(p => p.SpecialtieId, $"idx_{TableName}_fk_f_specialtie_id");

            builder.HasIndex(p => p.Course, $"idx_{TableName}_course_study");
            builder.HasIndex(p => p.StatusDeleted, $"idx_{TableName}_status_deleted");
        }
    }
}
