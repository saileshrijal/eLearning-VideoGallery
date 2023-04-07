using eLearning.Dtos;
using eLearning.Models;
using eLearning.Repository.Interface;
using eLearning.Service.Interface;

namespace eLearning.Service
{
    public class GradeService : IGradeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGradeRepository _gradeRepository;

        public GradeService(IUnitOfWork unitOfWork, IGradeRepository gradeRepository)
        {
            _unitOfWork = unitOfWork;
            _gradeRepository = gradeRepository;
        }

        public async Task CreateGrade(GradeDto gradeDto)
        {
            var grade = new Grade
            {
                Name = gradeDto.Name,
                Description = gradeDto.Description,
            };
            await _unitOfWork.CreateAsync(grade);
            await _unitOfWork.CommitAsync();

        }

        public async Task DeleteGrade(int id)
        {
            var grade = await _gradeRepository.GetById(id);
            await _unitOfWork.DeleteAsync(grade);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateGrade(int id, GradeDto gradeDto)
        {
            var grade = await _gradeRepository.GetById(id);
            grade.Name = gradeDto.Name;
            grade.Description = gradeDto.Description;
            await _unitOfWork.CommitAsync();
        }
    }
}