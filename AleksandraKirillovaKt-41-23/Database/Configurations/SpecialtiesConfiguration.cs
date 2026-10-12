using AleksandraKirillovaKt_41_23.Database.Helpers;
using AleksandraKirillovaKt_41_23.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AleksandraKirillovaKt_41_23.Database.Configurations
{
    public class SpecialtiesConfiguration: IEntityTypeConfiguration<Specialties>
    {
        private const string TableName = "cd_specialties";
        public void Configure(EntityTypeBuilder<Specialties> builder)
        {
            builder.ToTable(TableName);

            builder.HasKey(p => p.SpecialtieId)
                .HasName($"pk_{TableName}_specialtie_id");

            builder.Property(p => p.SpecialtieId)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.SpecialtieId)
                .HasColumnName("specialtie_id")
                .HasComment("Идентификатор специальности");

            builder.Property(p=> p.SpecialtieName)
                .IsRequired()
                .HasColumnName("c_specialtiename")
                .HasColumnType(ColumnType.String100).HasMaxLength(100)
                .HasComment("Наименование специальности");

            builder.Property(p => p.SpecialtieCode)
                .IsRequired()
                .HasColumnName("c_specialtiecode")
                .HasColumnType(ColumnType.String50).HasMaxLength(50)
                .HasComment("Код специальности");

            builder.HasIndex(p => p.SpecialtieName, $"idx_{TableName}_specialtiename");
        }
    }
}
