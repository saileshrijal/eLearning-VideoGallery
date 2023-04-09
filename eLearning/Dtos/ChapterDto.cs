namespace eLearning.Dtos
{
    public class ChapterDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? Unit { get; set; }
        public string? Description { get; set; }
        public int SubjectId { get; set; }
        public int GradeId { get; set; }
    }
}