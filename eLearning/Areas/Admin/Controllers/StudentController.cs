using AspNetCoreHero.ToastNotification.Abstractions;
using eLearning.Constant;
using eLearning.Dtos;
using eLearning.Models;
using eLearning.Repository.Interface;
using eLearning.Service.Interface;
using eLearning.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eLearning.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = UserRole.Admin)]
    public class StudentController : Controller
    {
        private readonly IUserService _userService;
        private readonly INotyfService _notfyService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IGradeRepository _gradeRepository;
        private readonly IUserRepository _userRepository;

        public StudentController(IUserService userService, INotyfService notfyService, UserManager<ApplicationUser> userManager, IGradeRepository gradeRepository, IUserRepository userRepository)
        {
            _userService = userService;
            _notfyService = notfyService;
            _userManager = userManager;
            _gradeRepository = gradeRepository;
            _userRepository = userRepository;
        }

        public async Task<IActionResult> Index()
        {
            var students = await _userRepository.GetStudentsWithGrade();
            var vm = students.Select(x => new ApplicationUserVM()
            {
                Id = x.Id,
                UserName = x.UserName,
                Email = x.Email,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Address = x.Address,
                CreatedAt = x.CreatedAt,
                GradeId = x.GradeId,
                Grade = x.Grade
            }).ToList();
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var grades = await _gradeRepository.GetAll();
            var vm = new ApplicationUserVM()
            {
                Grades = grades.Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList()
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ApplicationUserVM vm)
        {
            if (!ModelState.IsValid) { return View(vm); }
            try
            {

                var userDto = new UserDto()
                {
                    UserName = vm.UserName,
                    Email = vm.Email,
                    FirstName = vm.FirstName,
                    LastName = vm.LastName,
                    Address = vm.Address,
                    GradeId = vm.GradeId,
                    UserRole = UserRole.Student,
                    Password = vm.Password,
                };
                await _userService.Create(userDto);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _notfyService.Error(ex.Message);
                return View(vm);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var student = await _userRepository.GetStudentWithGradeById(id);
            var grades = await _gradeRepository.GetAll();
            var vm = new ApplicationUserVM()
            {
                Grades = grades.Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList(),
                FirstName = student.FirstName,
                LastName = student.LastName,
                Address = student.Address,
                Id = student.Id,
                Email = student.Email,
                UserName = student.UserName,
                CreatedAt = student.CreatedAt,
                GradeId = student.GradeId,
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ApplicationUserVM vm)
        {
            if (!ModelState.IsValid) { return View(vm); }
            try
            {
                var userDto = new UserDto()
                {
                    FirstName = vm.FirstName,
                    LastName = vm.LastName,
                    Address = vm.Address,
                    GradeId = vm.GradeId,
                    UserRole = UserRole.Student,
                };
                await _userService.Edit(vm.Id!, userDto);
                _notfyService.Success("Student updated successfully");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _notfyService.Error(ex.Message);
                return View(vm);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            var student = await _userRepository.GetStudentWithGradeById(id);
            var grades = await _gradeRepository.GetAll();
            var vm = new ApplicationUserVM()
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                Address = student.Address,
                Id = student.Id,
                Email = student.Email,
                UserName = student.UserName,
                CreatedAt = student.CreatedAt,
                GradeId = student.GradeId,
                Grade = student.Grade
            };
            return View(vm);
        }
    }
}
