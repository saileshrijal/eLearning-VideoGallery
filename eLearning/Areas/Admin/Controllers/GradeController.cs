using AspNetCoreHero.ToastNotification.Abstractions;
using eLearning.Dtos;
using eLearning.Models;
using eLearning.Repository.Interface;
using eLearning.Service.Interface;
using eLearning.ViewModels;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace eLearning.Area.Controllers
{
    [Area("Admin")]
    public class GradeController : Controller
    {
        private readonly IGradeService _gradeService;
        private readonly IGradeRepository _gradeRepository;

        public INotyfService _notifService { get; }

        public GradeController(IGradeService gradeService, IGradeRepository gradeRepository, INotyfService notifService)
        {
            _gradeService = gradeService;
            _gradeRepository = gradeRepository;
            _notifService = notifService;
        }

        public async Task<IActionResult> Index(string? search, int? page)
        {
            var grades = await _gradeRepository.GetAllGrades(search, page);
            ViewBag.Search = search;
            var vm = grades.Select(x => new GradeVM
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                CreatedAt = x.CreatedAt
            });
            return View(vm);
        }

        public IActionResult Create()
        {
            return View(new GradeVM());
        }

        [HttpPost]
        public async Task<IActionResult> Create(GradeVM vm)
        {
            if (!ModelState.IsValid) { return View(vm); }
            try
            {
                var gradeDto = new GradeDto
                {
                    Name = vm.Name,
                    Description = vm.Description
                };
                await _gradeService.CreateGrade(gradeDto);
                _notifService.Success("Grade created successfully");
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
            var grade = await _gradeRepository.GetById(id);
            var vm = new GradeVM
            {
                Id = grade.Id,
                Name = grade.Name,
                Description = grade.Description
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(GradeVM vm)
        {
            if (ModelState.IsValid)
            {
                var gradeDto = new GradeDto
                {
                    Id = vm.Id,
                    Name = vm.Name,
                    Description = vm.Description
                };
                await _gradeService.UpdateGrade(vm.Id, gradeDto);
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        public async Task<IActionResult> Details(int id)
        {
            var grade = await _gradeRepository.GetById(id);
            var vm = new GradeVM()
            {
                Id = grade.Id,
                Name = grade.Name,
                Description = grade.Description,
                CreatedAt = grade.CreatedAt
            };
            return View(vm);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var grade = await _gradeRepository.GetById(id);
            var vm = new GradeVM()
            {
                Id = grade.Id,
                Name = grade.Name,
                Description = grade.Description,
                CreatedAt = grade.CreatedAt
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(GradeVM vm)
        {
            await _gradeService.DeleteGrade(vm.Id);
            return RedirectToAction(nameof(Index));
        }
    }
}