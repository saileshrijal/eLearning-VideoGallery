using Microsoft.AspNetCore.Identity;

namespace eLearning.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public string? StudentCode { get; set; }
        public int? GradeId { get; set; }
        public Grade? Grade { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}