using ClassesManager.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassesManager.Services
{
    public class StorageService
    {
        private List<SubjectDBModel> _subjects;
        private List<ClassesDBModel> _classes;

        private void LoadData()
        {
            if (_subjects != null && _classes != null)
                return;
            _subjects = FakeStorage.Subjects.ToList();
            _classes = FakeStorage.Classes.ToList();
        }

        public IEnumerable<SubjectDBModel> GetAllSubjects()
        {
            LoadData();
            var resultList = new List<SubjectDBModel>();
            foreach (var subject in _subjects)
            {
                resultList.Add(subject);
            }
            return resultList;
        }

        public IEnumerable<ClassesDBModel> GetAllClasses(Guid subjectId)
        {
            LoadData();
            var resultList = new List<ClassesDBModel>();

            foreach (var cls in _classes)
            {
                if (cls.SubjectId == subjectId)
                {
                    resultList.Add(cls);
                }
            }
            return resultList;
        }
    }
}
