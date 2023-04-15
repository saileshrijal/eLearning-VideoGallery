using eLearning.Constant;
using eLearning.Models;
using eLearning.Repository.Interface;
using eLearning.Service.Interface;
using eLearning.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace eLearning.Areas.Student.Controllers
{
    [Area("Student")]
    [Authorize(Roles = UserRole.Student)]
    public class SubjectController : Controller
    {
        private readonly ISubjectGradeRepository _subjectGradeRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public SubjectController(ISubjectGradeRepository subjectGradeRepository, UserManager<ApplicationUser> userManager)
        {
            _subjectGradeRepository = subjectGradeRepository;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var subjects = await _subjectGradeRepository.GetSubjectsByGradeId((int)currentUser!.GradeId!);
            var vm = subjects.Select(x => new SubjectVM()
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code,
                Description = x.Description,
                CreatedAt = x.CreatedAt,
            }).ToList();
            return View(vm);
        }
    }
}
