using eLearning.Models;
using eLearning.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eLearning.ViewModels;
public class AssignSubjectVM
{
    public Grade? Grade { get; set; }

    public List<int>? SubjectIds{ get; set; }

    public List<SelectListItem> SelectSubjects { get; set; } = new List<SelectListItem>();
}