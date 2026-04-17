using ClassesManager.CommonComponents.Enums;
using ClassesManager.DBModels;
using ClassesManager.DTOModels;
using ClassesManager.Repositories;

namespace ClassesManager.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly IStorageRepository _storageRepository;

        public SubjectService(IStorageRepository storageRepository)
        {
            _storageRepository = storageRepository;
        }

        public async Task<IEnumerable<SubjectListDTO>> GetSubjectsListAsync()
        {
            await _storageRepository.InitializeDataAsync();
            // calling async method via await
            var dbSubjects = await _storageRepository.GetAllSubjectsAsync();

            return dbSubjects.Select(s => new SubjectListDTO
            {
                Id = s.Id,
                Name = s.Name,
                FieldOfKnowledge = s.FieldOfKnowledge.ToString(),
                ECTS = s.ECTS
            });
        }

        public async Task<SubjectDetailsDTO> GetSubjectDetailsAsync(Guid subjectId)
        {
            var dbSubject = await _storageRepository.GetSubjectByIdAsync(subjectId);
            if (dbSubject == null) return null;

            var dbClasses = await _storageRepository.GetAllClassesAsync(subjectId);

            var classesDTO = dbClasses.Select(c => new ClassListDTO
            {
                Id = c.Id,
                Theme = c.ThemeOfClass,
                Date = c.Date.ToString("dd.MM.yyyy"),
                Type = c.TypeOfClass.ToString()
            }).ToList();

            int totalMinutes = (int)dbClasses.Sum(c => (c.EndTime - c.StartTime).TotalMinutes);

            return new SubjectDetailsDTO
            {
                Id = dbSubject.Id,
                Name = dbSubject.Name,
                FieldOfKnowledge = dbSubject.FieldOfKnowledge.ToString(),
                ECTS = dbSubject.ECTS,
                Classes = classesDTO,
                TotalDuration = totalMinutes
            };
        }

        public async Task DeleteSubjectAsync(Guid id)
        {
            // async deleting
            await _storageRepository.DeleteSubjectAsync(id);
        }

        public async Task AddSubjectAsync(SubjectListDTO subject)
        {
            var dbModel = new SubjectDBModel(subject.Name, subject.ECTS, Enum.Parse<FieldOfKnowledge>(subject.FieldOfKnowledge));
            await _storageRepository.AddSubjectAsync(dbModel);
        }

        public async Task UpdateSubjectAsync(SubjectListDTO subject)
        {
            var dbModel = await _storageRepository.GetSubjectByIdAsync(subject.Id);
            if (dbModel != null)
            {
                dbModel.Name = subject.Name;
                dbModel.ECTS = subject.ECTS;
                dbModel.FieldOfKnowledge = Enum.Parse<FieldOfKnowledge>(subject.FieldOfKnowledge);
                await _storageRepository.UpdateSubjectAsync(dbModel);
            }
        }
    }
}
