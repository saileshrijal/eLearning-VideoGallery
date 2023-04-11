using System.ComponentModel.DataAnnotations;
using eLearning.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eLearning.ViewModels
{
    public class ChapterVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string? Name { get; set; }
        public string? Unit { get; set; }
        public string? Description { get; set; }

        [Required(ErrorMessage = "Subject is required")]
        public int SubjectId { get; set; }

        [Required(ErrorMessage = "Grade is required")]
        public int GradeId { get; set; }

        public DateTime CreatedAt { get; set; }

        public Subject? Subject { get; set; }

        public Grade? Grade { get; set; }

        public List<SelectListItem>? Subjects { get; set; } = new List<SelectListItem>();

        public List<SelectListItem>? Grades { get; set; } = new List<SelectListItem>();
    }
}