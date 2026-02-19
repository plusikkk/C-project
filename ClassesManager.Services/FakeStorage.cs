using ClassesManager.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassesManager.Services
{
    internal static class FakeStorage
    {
        private static readonly List<SubjectDBModel> _subjects;
        private static readonly List<ClassesDBModel> _classes;

        internal static IEnumerable<SubjectDBModel> Subjects
        {
            get
            {
                return _subjects.ToList();
            }
        }

        internal static IEnumerable<ClassesDBModel> Classes
        {
            get
            {
                return _classes.ToList();
            }
        }

        static FakeStorage()
        {
            

        }
    }
}
