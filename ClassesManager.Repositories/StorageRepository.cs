using ClassesManager.DBModels;
using System.Text.Json;
using ClassesManager.CommonComponents.Enums;

namespace ClassesManager.Repositories
{
    public class StorageRepository : IStorageRepository
    {
        //paths to files in the local folder
        private readonly string _subjectsFilePath;
        private readonly string _classesFilePath;

        public StorageRepository()
        {
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            _subjectsFilePath = Path.Combine(appDataPath, "subjects.json");
            _classesFilePath = Path.Combine(appDataPath, "classes.json");
        }

        #region Helper Methods for JSON Read/Write
        private async Task<List<SubjectDBModel>> ReadSubjectsAsync()
        {
            if (!File.Exists(_subjectsFilePath)) return new List<SubjectDBModel>();
            string json = await File.ReadAllTextAsync(_subjectsFilePath);
            return JsonSerializer.Deserialize<List<SubjectDBModel>>(json) ?? new List<SubjectDBModel>();
        }

        private async Task WriteSubjectsAsync(List<SubjectDBModel> subjects)
        {
            string json = JsonSerializer.Serialize(subjects, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(_subjectsFilePath, json);
        }

        private async Task<List<ClassesDBModel>> ReadClassesAsync()
        {
            if (!File.Exists(_classesFilePath)) return new List<ClassesDBModel>();
            string json = await File.ReadAllTextAsync(_classesFilePath);
            return JsonSerializer.Deserialize<List<ClassesDBModel>>(json) ?? new List<ClassesDBModel>();
        }

        private async Task WriteClassesAsync(List<ClassesDBModel> classes)
        {
            string json = JsonSerializer.Serialize(classes, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(_classesFilePath, json);
        }
        #endregion

        // method for writing initial data on first run
        public async Task InitializeDataAsync()
        {
            if (!File.Exists(_subjectsFilePath) || !File.Exists(_classesFilePath))
            {
                var initialSubjects = new List<SubjectDBModel>
                {
                    new SubjectDBModel { Id = Guid.NewGuid(), Name = "Object-Oriented Programming", ECTS = 5.5f, FieldOfKnowledge = (FieldOfKnowledge)12 },
                    new SubjectDBModel { Id = Guid.NewGuid(), Name = "Databases", ECTS = 4.0f, FieldOfKnowledge = (FieldOfKnowledge)12 }
                };

                var initialClasses = new List<ClassesDBModel>
                {
                    new ClassesDBModel { Id = Guid.NewGuid(), SubjectId = initialSubjects[0].Id, ThemeOfClass = "Polymorphism", TypeOfClass = (TypeOfClass)1, Date = DateOnly.FromDateTime(DateTime.Now.AddDays(1)), StartTime = new TimeOnly(8, 30, 0), EndTime = new TimeOnly(10, 5, 0) },
                    new ClassesDBModel { Id = Guid.NewGuid(), SubjectId = initialSubjects[1].Id, ThemeOfClass = "SQL Queries", TypeOfClass = (TypeOfClass)2, Date = DateOnly.FromDateTime(DateTime.Now.AddDays(2)), StartTime = new TimeOnly(10, 20, 0), EndTime = new TimeOnly(11, 55, 0) }
                };

                await WriteSubjectsAsync(initialSubjects);
                await WriteClassesAsync(initialClasses);
            }
        }

        #region Subjects CRUD
        public async Task<IEnumerable<SubjectDBModel>> GetAllSubjectsAsync()
        {
            return await ReadSubjectsAsync();
        }

        public async Task<SubjectDBModel> GetSubjectByIdAsync(Guid id)
        {
            var subjects = await ReadSubjectsAsync();
            return subjects.FirstOrDefault(s => s.Id == id);
        }

        public async Task AddSubjectAsync(SubjectDBModel subject)
        {
            var subjects = await ReadSubjectsAsync();
            subject.Id = Guid.NewGuid();
            subjects.Add(subject);
            await WriteSubjectsAsync(subjects);
        }

        public async Task UpdateSubjectAsync(SubjectDBModel subject)
        {
            var subjects = await ReadSubjectsAsync();
            var existing = subjects.FirstOrDefault(s => s.Id == subject.Id);
            if (existing != null)
            {
                existing.Name = subject.Name;
                existing.ECTS = subject.ECTS;
                existing.FieldOfKnowledge = subject.FieldOfKnowledge;
                await WriteSubjectsAsync(subjects);
            }
        }

        public async Task DeleteSubjectAsync(Guid id)
        {
            // deleting the subject
            var subjects = await ReadSubjectsAsync();
            var subjectToRemove = subjects.FirstOrDefault(s => s.Id == id);
            if (subjectToRemove != null)
            {
                subjects.Remove(subjectToRemove);
                await WriteSubjectsAsync(subjects);

                // deleting descendants
                var classes = await ReadClassesAsync();
                classes.RemoveAll(c => c.SubjectId == id);
                await WriteClassesAsync(classes);
            }
        }
        #endregion

        #region Classes CRUD
        public async Task<IEnumerable<ClassesDBModel>> GetAllClassesAsync(Guid subjectId)
        {
            var classes = await ReadClassesAsync();
            return classes.Where(c => c.SubjectId == subjectId);
        }

        public async Task<ClassesDBModel> GetClassByIdAsync(Guid classId)
        {
            var classes = await ReadClassesAsync();
            return classes.FirstOrDefault(c => c.Id == classId);
        }

        public async Task AddClassAsync(ClassesDBModel classModel)
        {
            var classes = await ReadClassesAsync();
            classModel.Id = Guid.NewGuid();
            classes.Add(classModel);
            await WriteClassesAsync(classes);
        }

        public async Task UpdateClassAsync(ClassesDBModel classModel)
        {
            var classes = await ReadClassesAsync();
            var existing = classes.FirstOrDefault(c => c.Id == classModel.Id);
            if (existing != null)
            {
                existing.ThemeOfClass = classModel.ThemeOfClass;
                existing.TypeOfClass = classModel.TypeOfClass;
                existing.Date = classModel.Date;
                existing.StartTime = classModel.StartTime;
                existing.EndTime = classModel.EndTime;
                await WriteClassesAsync(classes);
            }
        }

        public async Task DeleteClassAsync(Guid id)
        {
            var classes = await ReadClassesAsync();
            var classToRemove = classes.FirstOrDefault(c => c.Id == id);
            if (classToRemove != null)
            {
                classes.Remove(classToRemove);
                await WriteClassesAsync(classes);
            }
        }
        #endregion
    }
}