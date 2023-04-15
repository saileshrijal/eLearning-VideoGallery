using eLearning.Constant;
using eLearning.Models;
using eLearning.Repository.Interface;
using eLearning.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace eLearning.Areas.Student.Controllers
{
    [Area("Student")]
    [Authorize(Roles = UserRole.Student)]
    public class ChapterController : Controller
    {
        private readonly IChapterRepository _chapterRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public ChapterController(IChapterRepository chapterRepository, UserManager<ApplicationUser> userManager)
        {
            _chapterRepository = chapterRepository;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(int subjectId)
        {
            if(subjectId == 0)
            {
                return NotFound();
            }
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var chapters = await _chapterRepository.GetChaptersBySubjectIdAndGradeId(subjectId, (int)currentUser!.GradeId!);
            var vm = chapters.Select(x => new ChapterVM()
            {
                Id = x.Id,
                Name = x.Name,
                GradeId = x.GradeId,
                SubjectId = x.SubjectId,
                Subject = x.Subject
            }).ToList();
            return View(vm);
        }
    }
}
