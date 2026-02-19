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
        public Guid Id { get; set; }
        public Guid SubjectId { get; set; }
        public DateOnly Date {  get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public string ThemeOfClass { get; set; }
        public TypeOfClass TypeOfClass { get; set; }
    }
}
