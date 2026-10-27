using AleksandraKirillovaKt_41_23.Models;

namespace AleksandraKirillovaKt_41_23.Filters.GradeFilter
{
    public class GradeAddUpFilter
    {
        public int GradeId { get; set; }
        public int Score { get; set; }
        public DateTime GradeDate { get; set; }

        public int StudentId { get; set; }

        public int DisciplineId { get; set; }
    }
}
