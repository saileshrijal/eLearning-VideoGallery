using AspNetCoreHero.ToastNotification.Abstractions;
using eLearning.Dtos;
using eLearning.Helper.Interface;
using eLearning.Repository.Interface;
using eLearning.Service.Interface;
using eLearning.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using X.PagedList;

namespace eLearning.Area.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class VideoController : Controller
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly IGradeRepository _gradeRepository;
        private readonly IChapterRepository _chapterRepository;
        private readonly ISubjectGradeRepository _subjectGradeRepository;
        private readonly IVideoRepository _videoRepository;
        private readonly IVideoService _videoService;
        private readonly IFileHelper _fileHelper;

        public INotyfService _notifService { get; }

        public VideoController(INotyfService notifService, ISubjectRepository subjectRepository, IGradeRepository gradeRepository, IChapterRepository chapterRepository, IVideoService videoService, ISubjectGradeRepository subjectGradeRepository, IVideoRepository videoRepository, IFileHelper fileHelper)
        {
            _subjectRepository = subjectRepository;
            _gradeRepository = gradeRepository;
            _chapterRepository = chapterRepository;
            _videoService = videoService;
            _notifService = notifService;
            _subjectGradeRepository = subjectGradeRepository;
            _videoRepository = videoRepository;
            _fileHelper = fileHelper;
        }

        public async Task<IActionResult> Index(string? search, int? page)
        {
            var videos = await _videoRepository.GetAllVideos(search, page);
            ViewBag.Search = search;
            var vm = videos.Select(x => new VideoVM
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                ThumbnailUrl = x.ThumbnailUrl,
                VideoUrl = x.VideoUrl,
                CreatedAt = x.CreatedAt,
                Chapter = x.Chapter,
                Grade = x.Grade,
                Subject = x.Subject,
                SubjectId = x.SubjectId,
                GradeId = x.GradeId,
                ChapterId = x.ChapterId,
            });
            return View(vm);
        }

        public async Task<IActionResult> Create()
        {
            var grades = await _gradeRepository.GetAll();
            var subjects = await _subjectRepository.GetAll();
            var chapters = await _chapterRepository.GetAll();

            var vm = new VideoVM
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
        public async Task<IActionResult> Create(VideoVM vm)
        {
            if (!ModelState.IsValid) { return View(vm); }
            try
            {

                var videoDto = new VideoDto
                {
                    Title = vm.Title,
                    Description = vm.Description,
                    ThumbnailUrl = vm.ThumbnailUrl,
                    VideoUrl = vm.VideoUrl,
                    SubjectId = vm.SubjectId,
                    GradeId = vm.GradeId,
                    ChapterId = vm.ChapterId,
                };
                if (vm.Video != null)
                {
                    var videoUrl = await _fileHelper.Save(vm.Video, "Videos");
                    videoDto.VideoUrl = videoUrl;
                }
                if (vm.Thumbnail != null)
                {
                    var thumbnailUrl = await _fileHelper.Save(vm.Thumbnail, "Thumbnails");
                    videoDto.ThumbnailUrl = thumbnailUrl;
                }
                await _videoService.CreateVideo(videoDto);
                _notifService.Success("Video uploaded successfully");
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
            var video = await _videoRepository.GetById(id);
            var gradesList = await _gradeRepository.GetAll();
            var subjectsList = await _subjectRepository.GetAll();
            var chaptersList = await _chapterRepository.GetAll();
            var vm = new VideoVM
            {
                Id = video.Id,
                Title = video.Title,
                Description = video.Description,
                ThumbnailUrl = video.ThumbnailUrl,
                VideoUrl = video.VideoUrl,
                SubjectId = video.SubjectId,
                GradeId = video.GradeId,
                ChapterId = video.ChapterId,
                Grades = gradesList.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                }).ToList()
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(VideoVM vm)
        {
            if (ModelState.IsValid)
            {
                var videoDto = new VideoDto
                {
                    Id = vm.Id,
                    Title = vm.Title,
                    Description = vm.Description,
                    ThumbnailUrl = vm.ThumbnailUrl,
                    VideoUrl = vm.VideoUrl,
                    SubjectId = vm.SubjectId,
                    GradeId = vm.GradeId,
                    ChapterId = vm.ChapterId,
                };
                await _videoService.UpdateVideo(vm.Id, videoDto);
                _notifService.Success("Video updated successfully");
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        public async Task<IActionResult> Details(int id)
        {
            var video = await _videoRepository.GetVideoById(id);
            var vm = new VideoVM()
            {
                Id = video.Id,
                Title = video.Title,
                Description = video.Description,
                ThumbnailUrl = video.ThumbnailUrl,
                VideoUrl = video.VideoUrl,
                SubjectId = video.SubjectId,
                GradeId = video.GradeId,
                ChapterId = video.ChapterId,
                Subject = video.Subject,
                Grade = video.Grade,
                Chapter = video.Chapter,
                CreatedAt = video.CreatedAt
            };
            return View(vm);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var video = await _videoRepository.GetVideoById(id);
            var vm = new VideoVM()
            {
                Id = video.Id,
                Title = video.Title,
                Description = video.Description,
                ThumbnailUrl = video.ThumbnailUrl,
                VideoUrl = video.VideoUrl,
                SubjectId = video.SubjectId,
                GradeId = video.GradeId,
                ChapterId = video.ChapterId,
                Subject = video.Subject,
                Grade = video.Grade,
                Chapter = video.Chapter,
                CreatedAt = video.CreatedAt
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(VideoVM vm)
        {
            await _videoService.DeleteVideo(vm.Id);
            _notifService.Success("Video deleted successfully");
            return RedirectToAction(nameof(Index));
        }

        #region API CALLS
        [HttpGet]

        public async Task<IActionResult> GetChaptersBySubjectIdAndGradeId(int gradeId, int subjectId)
        {
            var chapters = await _chapterRepository.GetChaptersBySubjectIdAndGradeId(gradeId, subjectId);
            return Json(chapters.Select(x=> new
            {
                id = x.Id,
                name = x.Name,
            }));
        }
        #endregion
    }
}