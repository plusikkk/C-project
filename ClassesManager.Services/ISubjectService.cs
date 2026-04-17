using ClassesManager.DTOModels;

namespace ClassesManager.Services
{
    public interface ISubjectService
    {
        Task<IEnumerable<SubjectListDTO>> GetSubjectsListAsync();
        Task<SubjectDetailsDTO> GetSubjectDetailsAsync(Guid subjectId);
        Task DeleteSubjectAsync(Guid id);
        Task AddSubjectAsync(SubjectListDTO subject);
        Task UpdateSubjectAsync(SubjectListDTO subject);
    }
}