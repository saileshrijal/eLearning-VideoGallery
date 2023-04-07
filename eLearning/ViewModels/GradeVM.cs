using System.ComponentModel.DataAnnotations;

namespace eLearning.ViewModels;

public class GradeVM
{
    public int Id { get; set; }

    [Required]
    public string? Name { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}