using eLearning.Models;
using eLearning.Repository.Interface;
using eLearning.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace eLearning.Areas.Student.Controllers
{
    [Area("Student")]
    public class VideoController : Controller
    {
        private readonly IVideoRepository _videoRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public VideoController(IVideoRepository videoRepository, UserManager<ApplicationUser> userManager)
        {
            _videoRepository = videoRepository;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(int subjectId, int chapterId)
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var videos = await _videoRepository.GetVideosByGradeIdSubjectIdChapterId((int)currentUser!.GradeId!, subjectId, chapterId);
            var vm = videos.Select(x=> new VideoVM()
            {
                Id = x.Id,
                Title = x.Title,
                GradeId = x.GradeId,
                ChapterId = x.ChapterId,
                CreatedAt = x.CreatedAt,
                Description = x.Description,
                ThumbnailUrl = x.ThumbnailUrl,
            }).ToList();
            return View(vm);
        }

        public async Task<IActionResult> Details(int id)
        {
            var video = await _videoRepository.GetVideoById(id);
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            if (video.GradeId != currentUser!.GradeId)
            {
                return Content("You are not authorized");
            }
            var vm = new VideoVM()
            {
                Id = video.Id,
                Title = video.Title,
                Description = video.Description,
                VideoUrl = video.VideoUrl,
                GradeId = video.GradeId,
                ChapterId = video.ChapterId,
                CreatedAt = video.CreatedAt,
                Subject = video.Subject,
                Grade = video.Grade,
                Chapter = video.Chapter,
                ThumbnailUrl = video.ThumbnailUrl,
            };
            return View(vm);
        }
    }
}
