using AleksandraKirillovaKt_41_23.Database.Helpers;
using AleksandraKirillovaKt_41_23.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AleksandraKirillovaKt_41_23.Database.Configurations
{
    public class DisciplinesConfiguration : IEntityTypeConfiguration<Disciplines>
    {
        private const string TableName = "cd_discipline";
        public void Configure(EntityTypeBuilder<Disciplines> builder)
        {
            builder.ToTable(TableName);

            builder.HasKey(p => p.DisciplineId)
                .HasName($"pk_{TableName}_discipline_id");

            builder.Property(p => p.DisciplineId)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.DisciplineId)
                .HasColumnName("discipline_id")
                .HasComment("Идентификатор дисциплины");

            builder.Property(p => p.DisciplineName)
                .IsRequired()
                .HasColumnName("c_disciplinename")
                .HasColumnType(ColumnType.String100).HasMaxLength(100)
                .HasComment("Наименование дисциплины");

            builder.Property(p => p.DisciplineDirection)
                .IsRequired()
                .HasColumnName("c_disciplinedirection")
                .HasColumnType(ColumnType.String50).HasMaxLength(50)
                .HasComment("Направление дисциплины");

            builder.Property(p => p.StatusDeleted)
                .IsRequired()
                .HasColumnName("b_statusdeleted")
                .HasColumnType(ColumnType.Bool).HasDefaultValue(false)
                .HasComment("Статус удаления");

            builder.HasIndex(p => p.DisciplineDirection, $"idx_{TableName}_direction");
        }
    }
}
