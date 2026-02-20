using ClassesManager.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassesManager.Services
{
    //Service for interacting with storage (FakeStorage)
    public class StorageService
    {
        private List<SubjectDBModel> _subjects;
        private List<ClassesDBModel> _classes;

        //Loading data from storage 
        private void LoadData()
        {
            if (_subjects != null && _classes != null)
                return;
            _subjects = FakeStorage.Subjects.ToList();
            _classes = FakeStorage.Classes.ToList();
        }

        //Returning all available subjects
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

        //Returning all available classes connected with chosen subject
        public IEnumerable<ClassesDBModel> GetAllClasses(Guid subjectId)
        {
            LoadData();
            var resultList = new List<ClassesDBModel>();

            //Classes filter (all classes that belong to chosen subject)
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
