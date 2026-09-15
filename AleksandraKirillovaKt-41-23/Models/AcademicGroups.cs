using System.Data;

namespace AleksandraKirillovaKt_41_23.Models
{
    public class AcademicGroups
    {
        public int AcademicGroupId { get; set; }
        public int Course { get; set; }
        public string? AcademicGroupName { get; set; }
        public bool StatusDeleted { get; set; }

        public int SpecialtieId { get; set; }
        public Specialties? Specialties { get; set; }

    }
}
