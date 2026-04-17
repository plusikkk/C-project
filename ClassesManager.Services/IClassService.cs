using ClassesManager.DTOModels;

namespace ClassesManager.Services
{
    public interface IClassService
    {
        Task<ClassDetailsDTO> GetClassDetailsAsync(Guid classId);
        Task AddClassAsync(Guid subjectId, string theme, int typeIndex, DateTime date, TimeSpan start, TimeSpan end);
        Task UpdateClassAsync(Guid classId, string theme, int typeIndex, DateTime date, TimeSpan start, TimeSpan end);
        Task DeleteClassAsync(Guid classId);
    }
}
