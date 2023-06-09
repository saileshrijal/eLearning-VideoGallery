namespace eLearning.Models
{
    public class Subject
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public List<SubjectGrade>? SubjectGrades { get; set; }
        public List<Chapter>? Chapters { get; set; }
    }
}