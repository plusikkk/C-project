using ClassesManager.CommonComponents.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassesManager.DBModels
{
    public class ClassesDBModel
    {
        public Guid Id { get; init;  } //Id is generated only once and cannot be changed
        public Guid SubjectId { get; init; } //Foreign key for connection with Subject
        public DateOnly Date {  get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public string ThemeOfClass { get; set; }
        public TypeOfClass TypeOfClass { get; set; }

        private ClassesDBModel() 
        { 
        
        }

        public ClassesDBModel(Guid subjectId, DateOnly date, TimeOnly startTime, TimeOnly endTime, string themeOfClass, TypeOfClass typeOfClass)
        {
            Id = Guid.NewGuid();
            SubjectId = subjectId;
            Date = date;
            StartTime = startTime;
            EndTime = endTime;
            ThemeOfClass = themeOfClass;
            TypeOfClass = typeOfClass;
        }
    }
}
