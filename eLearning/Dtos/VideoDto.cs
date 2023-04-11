namespace eLearning.Dtos
{
    public class VideoDto
    {
        public int Id { get; set; }
        public int? GradeId { get; set; }
        public int? SubjectId { get; set; }
        public int? ChapterId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? ThumbnailUrl { get; set; }
        public string? VideoUrl { get; set; }
    }
}