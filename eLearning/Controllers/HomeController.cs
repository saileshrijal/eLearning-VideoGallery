using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using eLearning.Models;
using eLearning.Repository.Interface;
using eLearning.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace eLearning.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly UserManager<ApplicationUser> _applicationUser;

    private readonly ISubjectRepository _subjectRepository;
	public HomeController(ILogger<HomeController> logger, ISubjectRepository subjectRepository, UserManager<ApplicationUser> applicationUser)
	{
		_logger = logger;
		_subjectRepository = subjectRepository;
		_applicationUser = applicationUser;
	}

	public IActionResult Index()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
