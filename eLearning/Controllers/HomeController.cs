using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using eLearning.Models;
using eLearning.Repository.Interface;
using eLearning.ViewModels;

namespace eLearning.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private readonly ISubjectRepository _subjectRepository;
    public HomeController(ILogger<HomeController> logger, ISubjectRepository subjectRepository)
    {
        _logger = logger;
        _subjectRepository = subjectRepository;
    }

    public async Task<IActionResult> Index()
    {
        var subjects = await _subjectRepository.GetAll();
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

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
