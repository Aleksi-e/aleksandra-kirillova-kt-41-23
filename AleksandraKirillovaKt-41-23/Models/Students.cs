using System.Text.RegularExpressions;

namespace AleksandraKirillovaKt_41_23.Models
{
    public class Students
    {
        public int StudentId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime BirthDate { get; set; }

        public int AcademicGroupId { get; set; }  
        public AcademicGroups? AcademicGroups { get; set; }       
        public bool StatusDeleted { get; set; }
    }
}
