using AspNetCoreHero.ToastNotification.Abstractions;
using eLearning.Constant;
using eLearning.Dtos;
using eLearning.Repository.Interface;
using eLearning.Service.Interface;
using eLearning.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;

namespace eLearning.Area.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = UserRole.Admin)]
    public class ChapterController : Controller
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly IGradeRepository _gradeRepository;
        private readonly IChapterRepository _chapterRepository;
        private readonly IChapterService _chapterService;
        private readonly ISubjectGradeRepository _subjectGradeRepository;

        public INotyfService _notifService { get; }

        public ChapterController(INotyfService notifService, ISubjectRepository subjectRepository, IGradeRepository gradeRepository, IChapterRepository chapterRepository, IChapterService chapterService, ISubjectGradeRepository subjectGradeRepository)
        {
            _subjectRepository = subjectRepository;
            _gradeRepository = gradeRepository;
            _chapterRepository = chapterRepository;
            _chapterService = chapterService;
            _notifService = notifService;
            _subjectGradeRepository = subjectGradeRepository;
        }

        public async Task<IActionResult> Index(string? search, int? page)
        {
            var chapters = await _chapterRepository.GetAllChapters(search, page);
            ViewBag.Search = search;
            var vm = chapters.Select(x => new ChapterVM
            {
                Id = x.Id,
                Name = x.Name,
                Unit = x.Unit,
                Description = x.Description,
                SubjectId = x.SubjectId,
                GradeId = x.GradeId,
                CreatedAt = x.CreatedAt,
                Subject = x.Subject,
                Grade = x.Grade
            });
            return View(vm);
        }

        public async Task<IActionResult> Create()
        {
            var grades = await _gradeRepository.GetAll();
            var subjects = await _subjectRepository.GetAll();

            var vm = new ChapterVM
            {
                Grades = grades.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                }).ToList(),
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ChapterVM vm)
        {
            if (!ModelState.IsValid) { return View(vm); }
            try
            {
                var chapterDto = new ChapterDto
                {
                    Name = vm.Name,
                    Unit = vm.Unit,
                    Description = vm.Description,
                    SubjectId = vm.SubjectId,
                    GradeId = vm.GradeId,
                };
                await _chapterService.CreateChapter(chapterDto);
                _notifService.Success("Chapter created successfully");
                return RedirectToAction(nameof(Create));
            }
            catch (Exception ex)
            {
                _notifService.Error(ex.Message);
                return View(vm);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var chapter = await _chapterRepository.GetById(id);
            var gradesList = await _gradeRepository.GetAll();
            var subjectsList = await _subjectRepository.GetAll();
            var vm = new ChapterVM()
            {
                Id = chapter.Id,
                Name = chapter.Name,
                Unit = chapter.Unit,
                Description = chapter.Description,
                SubjectId = chapter.SubjectId,
                GradeId = chapter.GradeId,
                Grades = gradesList.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name,
                    Selected = x.Id == chapter.GradeId
                }).ToList(),
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ChapterVM vm)
        {
            if (ModelState.IsValid)
            {
                var chapterDto = new ChapterDto
                {
                    Name = vm.Name,
                    Unit = vm.Unit,
                    Description = vm.Description,
                    SubjectId = vm.SubjectId,
                    GradeId = vm.GradeId,
                };
                await _chapterService.UpdateChapter(vm.Id, chapterDto);
                _notifService.Success("Chapter updated successfully");
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        public async Task<IActionResult> Details(int id)
        {
            var chapter = await _chapterRepository.GetChapterById(id);
            var vm = new ChapterVM()
            {
                Id = chapter.Id,
                Name = chapter.Name,
                Unit = chapter.Unit,
                Description = chapter.Description,
                SubjectId = chapter.SubjectId,
                GradeId = chapter.GradeId,
                CreatedAt = chapter.CreatedAt,
                Subject = chapter.Subject,
                Grade = chapter.Grade
            };
            return View(vm);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var chapter = await _chapterRepository.GetChapterById(id);
            var vm = new ChapterVM()
            {
                Id = chapter.Id,
                Name = chapter.Name,
                Unit = chapter.Unit,
                Description = chapter.Description,
                SubjectId = chapter.SubjectId,
                GradeId = chapter.GradeId,
                CreatedAt = chapter.CreatedAt,
                Subject = chapter.Subject,
                Grade = chapter.Grade
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(ChapterVM vm)
        {
            await _chapterService.DeleteChapter(vm.Id);
            _notifService.Success("Chapter deleted successfully");
            return RedirectToAction(nameof(Index));
        }

        #region API CALLS
        [HttpGet]
        public async Task<IActionResult> GetSubjectsByGradeId(int id)
        {
            var subjects = await _subjectGradeRepository.GetSubjectsByGradeId(id);
            return Json(subjects.Select(x=> new
            {
                id = x.Id,
                name = x.Name
            }));
        }
        #endregion
    }
}