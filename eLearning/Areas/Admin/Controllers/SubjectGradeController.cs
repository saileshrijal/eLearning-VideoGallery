using AspNetCoreHero.ToastNotification.Abstractions;
using eLearning.Models;
using eLearning.Repository.Interface;
using eLearning.Service.Interface;
using eLearning.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eLearning.Area.Controllers
{
    [Area("Admin")]
    public class SubjectGradeController : Controller
    {

        private readonly IGradeService _gradeService;
        private readonly IGradeRepository _gradeRepository;
        private readonly ISubjectRepository _subjectRepository;
        public INotyfService _notifService { get; }

        public SubjectGradeController(IGradeService gradeService, INotyfService notifService, IGradeRepository gradeRepository, ISubjectRepository subjectRepository)
        {
            _gradeService = gradeService;
            _notifService = notifService;
            _gradeRepository = gradeRepository;
            _subjectRepository = subjectRepository;
        }

        public async Task<IActionResult> Index(string? search, int? page)
        {
            var gradesWithSubjects = await _gradeRepository.GetAllGradesWithSubjects(search, page);
            ViewBag.Search = search;
            return View(gradesWithSubjects);
        }

        [HttpGet]
        public async Task<IActionResult> Assign(int id)
        {
            var grade = await _gradeRepository.GetGradeWithSubjects(id);

            if (grade == null)
            {
                return NotFound();
            }

            var subjects = await _subjectRepository.GetAll();

            var vm = new AssignSubjectVM
            {
                Grade = grade,
                SelectSubjects = subjects.Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Name,
                    Selected = grade.SubjectGrades!.Any(sg => sg.SubjectID == s.Id)
                }).ToList()
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Assign(AssignSubjectVM vm)
        {
            if (!ModelState.IsValid) { return View(vm); }

            try
            {
                await _gradeService.AsignSubjectsToGrade(vm.Grade!.Id, vm.SubjectIds!);
                _notifService.Success("Subjects assigned successfully");
            }
            catch (Exception ex)
            {
                _notifService.Error(ex.Message);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}