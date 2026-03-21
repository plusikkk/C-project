using ClassesManager.DBModels;

namespace ClassesManager.Repositories
{
    public class StorageRepository : IStorageRepository
    {
        public IEnumerable<SubjectDBModel> GetAllSubjects()
        {
            return FakeStorage.Subjects.ToList();
        }

        public IEnumerable<ClassesDBModel> GetAllClasses(Guid subjectId)
        {
            return FakeStorage.Classes.Where(c => c.SubjectId == subjectId).ToList();
        }

        public ClassesDBModel GetClassById(Guid classId)
        {
            return FakeStorage.Classes.FirstOrDefault(c => c.Id == classId);
        }
    }
}