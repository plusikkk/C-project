using ClassesManager.CommonComponents.Enums;
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
        internal static IEnumerable<SubjectDBModel> Subjects => _subjects.ToList();
        internal static IEnumerable<ClassesDBModel> Classes => _classes.ToList();

        static FakeStorage()
        {
            var math = new SubjectDBModel("Algorithm theory", 4.5f, FieldOfKnowledge.Mathematics);
            var prog = new SubjectDBModel("OOP", 6.0f, FieldOfKnowledge.Programming);
            var hist = new SubjectDBModel("History of Ukrain", 3.0f, FieldOfKnowledge.History);

            _subjects = new List<SubjectDBModel> { math, prog, hist };
            _classes = new List<ClassesDBModel>();

            for (int i = 1; i <= 10; i++)
            {
                _classes.Add(new ClassesDBModel(
                    prog.Id,
                    new DateOnly(2026, 2, i),
                    new TimeOnly(8, 30),
                    new TimeOnly(9, 50),
                    $"Lecture {i} OOP",
                    TypeOfClass.Lecture
                ));
            }

            _classes.Add(new ClassesDBModel(
                math.Id,
                new DateOnly(2026, 3, 15),
                new TimeOnly(11, 40),
                new TimeOnly(13, 00),
                "Merge sort",
                TypeOfClass.Seminar
            ));

            _classes.Add(new ClassesDBModel(
                math.Id,
                new DateOnly(2026, 3, 20),
                new TimeOnly(11, 40),
                new TimeOnly(13, 00),
                "Bubble sort",
                TypeOfClass.Practice
            ));
        }
    }
}
