using eLearning.Dtos;
using eLearning.Models;
using eLearning.Repository.Interface;
using eLearning.Service.Interface;

namespace eLearning.Service
{
    public class SubjectService : ISubjectService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISubjectRepository _subjectRepository;

        public SubjectService(IUnitOfWork unitOfWork, ISubjectRepository subjectRepository)
        {
            _unitOfWork = unitOfWork;
            _subjectRepository = subjectRepository;
        }

        public async Task CreateSubject(SubjectDto subjectDto)
        {
            var subject = new Subject
            {
                Name = subjectDto.Name,
                Code = subjectDto.Code,
                Description = subjectDto.Description,
            };
            await _unitOfWork.CreateAsync(subject);
            await _unitOfWork.CommitAsync();

        }

        public async Task DeleteSubject(int id)
        {
            var subject = await _subjectRepository.GetById(id);
            await _unitOfWork.DeleteAsync(subject);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateSubject(int id, SubjectDto subjectDto)
        {
            var subject = await _subjectRepository.GetById(id);
            subject.Name = subjectDto.Name;
            subject.Code = subjectDto.Code;
            subject.Description = subjectDto.Description;
            await _unitOfWork.CommitAsync();
        }
    }
}