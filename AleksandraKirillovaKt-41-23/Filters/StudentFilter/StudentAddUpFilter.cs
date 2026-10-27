using AleksandraKirillovaKt_41_23.Models;

namespace AleksandraKirillovaKt_41_23.Filters.StudentFilter
{
    public class StudentAddUpFilter
    {
        public int StudentId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime BirthDate { get; set; }

        public int AcademicGroupId { get; set; }
    }
}
