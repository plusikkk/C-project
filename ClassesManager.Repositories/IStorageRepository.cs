using ClassesManager.DBModels;

namespace ClassesManager.Repositories
{
    public interface IStorageRepository
    {
        IEnumerable<SubjectDBModel> GetAllSubjects();
        IEnumerable<ClassesDBModel> GetAllClasses(Guid subjectId);
        ClassesDBModel GetClassById(Guid classId);
    }
}
