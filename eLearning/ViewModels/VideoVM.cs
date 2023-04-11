using eLearning.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eLearning.ViewModels
{
    public class VideoVM
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

        public IFormFile? Thumbnail { get; set; }
        public IFormFile? Video { get; set; }

        public List<SelectListItem>? Grades { get; set; } = new List<SelectListItem>();

        public List<SelectListItem>? Subjects { get; set; } = new List<SelectListItem>();

        public List<SelectListItem>? Chapters { get; set; } = new List<SelectListItem>();
    }
}