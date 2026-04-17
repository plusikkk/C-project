using ClassesManager.DBModels;

namespace ClassesManager.Repositories
{
    public interface IStorageRepository
    {
        //method for writing initial data on first run
        Task InitializeDataAsync();

        //subjects
        Task<IEnumerable<SubjectDBModel>> GetAllSubjectsAsync();
        Task<SubjectDBModel> GetSubjectByIdAsync(Guid id);
        Task AddSubjectAsync(SubjectDBModel subject);
        Task UpdateSubjectAsync(SubjectDBModel subject);
        Task DeleteSubjectAsync(Guid id);

        //lessons
        Task<IEnumerable<ClassesDBModel>> GetAllClassesAsync(Guid subjectId);
        Task<ClassesDBModel> GetClassByIdAsync(Guid classId);
        Task AddClassAsync(ClassesDBModel classModel);
        Task UpdateClassAsync(ClassesDBModel classModel);
        Task DeleteClassAsync(Guid id);
    }
}
