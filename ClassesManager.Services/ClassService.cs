using ClassesManager.DTOModels;
using ClassesManager.Repositories;
using ClassesManager.DBModels;
using ClassesManager.CommonComponents.Enums;

namespace ClassesManager.Services
{
    public class ClassService : IClassService
    {
        private readonly IStorageRepository _storageRepository;

        public ClassService(IStorageRepository storageRepository)
        {
            _storageRepository = storageRepository;
        }

        public async Task<ClassDetailsDTO> GetClassDetailsAsync(Guid classId)
        {
            var dbClass = await _storageRepository.GetClassByIdAsync(classId);
            if (dbClass == null) return null;

            return new ClassDetailsDTO
            {
                Id = dbClass.Id,
                Theme = dbClass.ThemeOfClass,
                Type = dbClass.TypeOfClass.ToString(),
                Date = dbClass.Date.ToString("dd.MM.yyyy"),
                TimeRange = $"{dbClass.StartTime} - {dbClass.EndTime}",
                DurationMinutes = (int)(dbClass.EndTime - dbClass.StartTime).TotalMinutes
            };
        }

        // adding new class
        public async Task AddClassAsync(Guid subjectId, string theme, int typeIndex, DateTime date, TimeSpan start, TimeSpan end)
        {
            var newClass = new ClassesDBModel
            {
                SubjectId = subjectId,
                ThemeOfClass = theme,
                TypeOfClass = (TypeOfClass)typeIndex,
                Date = DateOnly.FromDateTime(date),
                StartTime = TimeOnly.FromTimeSpan(start),
                EndTime = TimeOnly.FromTimeSpan(end)
            };
            await _storageRepository.AddClassAsync(newClass);
        }

        // updating class
        public async Task UpdateClassAsync(Guid classId, string theme, int typeIndex, DateTime date, TimeSpan start, TimeSpan end)
        {
            var dbClass = await _storageRepository.GetClassByIdAsync(classId);
            if (dbClass != null)
            {
                dbClass.ThemeOfClass = theme;
                dbClass.TypeOfClass = (TypeOfClass)typeIndex;
                dbClass.Date = DateOnly.FromDateTime(date);
                dbClass.StartTime = TimeOnly.FromTimeSpan(start);
                dbClass.EndTime = TimeOnly.FromTimeSpan(end);
                await _storageRepository.UpdateClassAsync(dbClass);
            }
        }

        // deleting
        public async Task DeleteClassAsync(Guid classId)
        {
            await _storageRepository.DeleteClassAsync(classId);
        }
    }
}