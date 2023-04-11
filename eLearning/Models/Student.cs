namespace eLearning.Models
{
    public class Student : ApplicationUser
    {
        public string? StudentCode { get; set; }
        public string? GradeId { get; set; }
        public Grade? Grade { get; set; }
    }
}