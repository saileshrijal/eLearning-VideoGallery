using System.ComponentModel.DataAnnotations;

namespace eLearning.Models;

public class Grade
{
    public int Id { get; set; }

    public string? Name { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public List<SubjectGrade>? SubjectGrades { get; set; }

}