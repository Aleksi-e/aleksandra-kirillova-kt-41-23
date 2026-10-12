using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AleksandraKirillovaKt_41_23.Migrations
{
    /// <inheritdoc />
    public partial class CreateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cd_discipline",
                columns: table => new
                {
                    discipline_id = table.Column<int>(type: "int", nullable: false, comment: "Идентификатор дисциплины")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    c_disciplinename = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "Наименование дисциплины"),
                    c_disciplinedirection = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Направление дисциплины"),
                    b_statusdeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "Статус удаления")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cd_discipline_discipline_id", x => x.discipline_id);
                });

            migrationBuilder.CreateTable(
                name: "cd_specialties",
                columns: table => new
                {
                    specialtie_id = table.Column<int>(type: "int", nullable: false, comment: "Идентификатор специальности")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    c_specialtiename = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "Наименование специальности"),
                    c_specialtiecode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Код специальности")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cd_specialties_specialtie_id", x => x.specialtie_id);
                });

            migrationBuilder.CreateTable(
                name: "cd_academic_group",
                columns: table => new
                {
                    academic_group_id = table.Column<int>(type: "int", nullable: false, comment: "Идентификатор академической группы")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    n_course = table.Column<int>(type: "int", nullable: false, comment: "Курс обучения"),
                    c_academic_groupname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Наименование группы"),
                    b_statusdeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "Статус удаления"),
                    SpecialtieId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cd_academic_group_academic_group_id", x => x.academic_group_id);
                    table.ForeignKey(
                        name: "fk_f_specialtie_id",
                        column: x => x.SpecialtieId,
                        principalTable: "cd_specialties",
                        principalColumn: "specialtie_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "cd_student",
                columns: table => new
                {
                    student_id = table.Column<int>(type: "int", nullable: false, comment: "Идентификатор студента")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    c_student_firstname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Имя студента"),
                    c_student_lastname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Фамилия студента"),
                    d_birthdate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Дата рождения студента"),
                    AcademicGroupId = table.Column<int>(type: "int", nullable: false),
                    b_statusdeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "Статус удаления")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cd_student_student_id", x => x.student_id);
                    table.ForeignKey(
                        name: "fk_f_academic_groups_id",
                        column: x => x.AcademicGroupId,
                        principalTable: "cd_academic_group",
                        principalColumn: "academic_group_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cd_grades",
                columns: table => new
                {
                    grades_id = table.Column<int>(type: "int", nullable: false, comment: "Идентификатор оценок")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    n_score = table.Column<int>(type: "int", nullable: false, comment: "Оценка"),
                    d_gradesdate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Дата получения оценки"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    DisciplineId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cd_grades_grades_id", x => x.grades_id);
                    table.ForeignKey(
                        name: "fk_f_grades_disciplines_id",
                        column: x => x.DisciplineId,
                        principalTable: "cd_discipline",
                        principalColumn: "discipline_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_f_grades_students_id",
                        column: x => x.StudentId,
                        principalTable: "cd_student",
                        principalColumn: "student_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "idx_cd_academic_group_course_study",
                table: "cd_academic_group",
                column: "n_course");

            migrationBuilder.CreateIndex(
                name: "idx_cd_academic_group_fk_f_specialtie_id",
                table: "cd_academic_group",
                column: "SpecialtieId");

            migrationBuilder.CreateIndex(
                name: "idx_cd_academic_group_status_deleted",
                table: "cd_academic_group",
                column: "b_statusdeleted");

            migrationBuilder.CreateIndex(
                name: "idx_cd_discipline_direction",
                table: "cd_discipline",
                column: "c_disciplinedirection");

            migrationBuilder.CreateIndex(
                name: "idx_cd_grades_fk_f_disciplines_id",
                table: "cd_grades",
                column: "DisciplineId");

            migrationBuilder.CreateIndex(
                name: "idx_cd_grades_fk_f_students_id",
                table: "cd_grades",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "idx_cd_specialties_specialtiename",
                table: "cd_specialties",
                column: "c_specialtiename");

            migrationBuilder.CreateIndex(
                name: "idx_cd_student_fk_f_academic_groups_id",
                table: "cd_student",
                column: "AcademicGroupId");

            migrationBuilder.CreateIndex(
                name: "idx_cd_student_lastname",
                table: "cd_student",
                column: "c_student_lastname");

            migrationBuilder.CreateIndex(
                name: "idx_cd_student_polnoename",
                table: "cd_student",
                columns: new[] { "c_student_lastname", "c_student_firstname" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cd_grades");

            migrationBuilder.DropTable(
                name: "cd_discipline");

            migrationBuilder.DropTable(
                name: "cd_student");

            migrationBuilder.DropTable(
                name: "cd_academic_group");

            migrationBuilder.DropTable(
                name: "cd_specialties");
        }
    }
}
