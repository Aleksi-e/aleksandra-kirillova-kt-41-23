namespace AleksandraKirillovaKt_41_23.Models
{
    public class Grades //Оценки
    {
        public int GradeId {  get; set; }
        public int Score {  get; set; }
        public DateTime GradeDate { get; set; }

        public int StudentId { get; set; }
        public Students? Students { get; set; }

        public int DisciplineId { get; set; }
        public Disciplines? Disciplines { get; set; }
    }
}
