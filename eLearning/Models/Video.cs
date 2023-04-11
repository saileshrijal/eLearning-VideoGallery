namespace eLearning.Models
{
    public class Video
    {
        public int Id { get; set; }
        public int? GradeId { get; set; }
        public Grade? Grade { get; set; }
        public int? SubjectId { get; set; }
        public Subject? Subject { get; set; }
        public int? ChapterId { get; set; }
        public Chapter? Chapter { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? ThumbnailUrl { get; set; }
        public string? VideoUrl { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}