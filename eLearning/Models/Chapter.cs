namespace eLearning.Models
{
    public class Chapter
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Unit { get; set; }
        public string? Description { get; set; }
        public int SubjectId { get; set; }
        public virtual Subject? Subject { get; set; }
        public int GradeId { get; set; }
        public virtual Grade? Grade { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<Video>? Videos { get; set; }
    }
}