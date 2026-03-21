using ClassesManager.DTOModels;
using ClassesManager.Repositories;

namespace ClassesManager.Services
{
    public class SubjectService : ISubjectService
    {
        // Service acts with repository through the interface (Dependency Inversion)
        private readonly IStorageRepository _repository;

        public SubjectService(IStorageRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<SubjectListDTO> GetSubjectsList()
        {
            var dbSubjects = _repository.GetAllSubjects();
            var dtoList = new List<SubjectListDTO>();

            // Converts dbmodels to dto
            foreach (var dbModel in dbSubjects)
            {
                dtoList.Add(new SubjectListDTO
                {
                    Id = dbModel.Id,
                    Name = dbModel.Name,
                    ECTS = dbModel.ECTS,
                    FieldOfKnowledge = dbModel.FieldOfKnowledge.ToString()
                });
            }

            return dtoList;
        }

        public SubjectDetailsDTO GetSubjectDetails(Guid id)
        {
            // find the subject
            var dbSubject = _repository.GetAllSubjects().FirstOrDefault(s => s.Id == id);
            if (dbSubject == null) return null;

            // find the classes
            var dbClasses = _repository.GetAllClasses(id);

            var detailsDto = new SubjectDetailsDTO
            {
                Id = dbSubject.Id,
                Name = dbSubject.Name,
                ECTS = dbSubject.ECTS,
                FieldOfKnowledge = dbSubject.FieldOfKnowledge.ToString(),
                Classes = new List<ClassListDTO>()
            };

            int totalDuration = 0;

            // Converts classes to dto
            foreach (var cls in dbClasses)
            {
                int duration = (int)(cls.EndTime - cls.StartTime).TotalMinutes;
                totalDuration += duration;

                detailsDto.Classes.Add(new ClassListDTO
                {
                    Id = cls.Id,
                    Theme = cls.ThemeOfClass,
                    Date = cls.Date.ToString(),
                    Type = cls.TypeOfClass.ToString()
                });
            }

            detailsDto.TotalDuration = totalDuration;
            return detailsDto;
        }
    }
}
