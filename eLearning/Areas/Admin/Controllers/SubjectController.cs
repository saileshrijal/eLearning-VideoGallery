using AspNetCoreHero.ToastNotification.Abstractions;
using eLearning.Dtos;
using eLearning.Models;
using eLearning.Repository.Interface;
using eLearning.Service.Interface;
using eLearning.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using X.PagedList;

namespace eLearning.Area.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SubjectController : Controller
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly ISubjectService _subjectService;

        public INotyfService _notifService { get; }

        public SubjectController(ISubjectRepository subjectRepository, ISubjectService subjectService, INotyfService notifService)
        {
            _subjectRepository = subjectRepository;
            _subjectService = subjectService;
            _notifService = notifService;
        }

        public async Task<IActionResult> Index(string? search, int? page)
        {
            var subjects = await _subjectRepository.GetAllSubjects(search, page);
            ViewBag.Search = search;
            var vm = subjects.Select(x => new SubjectVM
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Code = x.Code,
                CreatedAt = x.CreatedAt
            });
            return View(vm);
        }

        public IActionResult Create()
        {
            return View(new SubjectVM());
        }

        [HttpPost]
        public async Task<IActionResult> Create(SubjectVM vm)
        {
            if (!ModelState.IsValid) { return View(vm); }
            try
            {
                var subjectDto = new SubjectDto
                {
                    Name = vm.Name,
                    Code = vm.Code,
                    Description = vm.Description,
                };
                await _subjectService.CreateSubject(subjectDto);
                _notifService.Success("Subject created successfully");
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
            var subject = await _subjectRepository.GetById(id);
            var vm = new SubjectVM()
            {
                Id = subject.Id,
                Name = subject.Name,
                Code = subject.Code,
                Description = subject.Description,
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SubjectVM vm)
        {
            if (ModelState.IsValid)
            {
                var subjectDto = new SubjectDto
                {
                    Name = vm.Name,
                    Code = vm.Code,
                    Description = vm.Description,
                };
                await _subjectService.UpdateSubject(vm.Id, subjectDto);
                _notifService.Success("Subject updated successfully");
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        public async Task<IActionResult> Details(int id)
        {
            var subject = await _subjectRepository.GetById(id);
            var vm = new SubjectVM()
            {
                Id = subject.Id,
                Name = subject.Name,
                Code = subject.Code,
                Description = subject.Description,
            };
            return View(vm);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var subject = await _subjectRepository.GetById(id);
            var vm = new SubjectVM()
            {
                Id = subject.Id,
                Name = subject.Name,
                Code = subject.Code,
                Description = subject.Description,
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(SubjectVM vm)
        {
            await _subjectService.DeleteSubject(vm.Id);
            _notifService.Success("Subject deleted successfully");
            return RedirectToAction(nameof(Index));
        }
    }
}