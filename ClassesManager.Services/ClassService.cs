using ClassesManager.DTOModels;
using ClassesManager.Repositories;
using System;

namespace ClassesManager.Services
{
    public class ClassService : IClassService
    {
        private readonly IStorageRepository _repository;

        // get repository through the id
        public ClassService(IStorageRepository repository)
        {
            _repository = repository;
        }

        public ClassDetailsDTO GetClassDetails(Guid classId)
        {
            var dbClass = _repository.GetClassById(classId);
            if (dbClass == null) return null;

            // converts dbmodel to dto
            return new ClassDetailsDTO
            {
                Id = dbClass.Id,
                Theme = dbClass.ThemeOfClass,
                Type = dbClass.TypeOfClass.ToString(),
                Date = dbClass.Date.ToString(),
                TimeRange = $"{dbClass.StartTime} - {dbClass.EndTime}",
                DurationMinutes = (int)(dbClass.EndTime - dbClass.StartTime).TotalMinutes
            };
        }
    }
}