using System.ComponentModel.DataAnnotations;
using eLearning.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eLearning.ViewModels
{
    public class ApplicationUserVM
    {
        public string? Id { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? UserRole { get; set; }

        [Required(ErrorMessage = "Grade is required")]
        public int? GradeId { get; set; }
        public Grade? Grade { get; set; }
        public List<SelectListItem>? Grades { get; set; }

        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$", ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one digit, one special character, and must be at least 8 characters long")]
        public string? Password { get; set; }
    }
}