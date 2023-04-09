using System.ComponentModel.DataAnnotations;
using eLearning.Models;
using X.PagedList;

namespace eLearning.ViewModels;

public class SubjectGradeVM
{
    public List<Grade>? Grades { get; set; }
    public List<Subject>? Subjects { get; set; }
}