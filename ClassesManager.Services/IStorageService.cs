using ClassesManager.DBModels;
using System;
using System.Collections.Generic;

namespace ClassesManager.Services
{
    public interface IStorageService
    {
        IEnumerable<SubjectDBModel> GetAllSubjects();
        IEnumerable<ClassesDBModel> GetAllClasses(Guid subjectId);
        void AddClass(ClassesDBModel newClass);
    }
}