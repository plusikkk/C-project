using ClassesManager.CommonComponents.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassesManager.DBModels
{
    public class SubjectDBModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public float ECTS { get; set; }
        public FieldOfKnowledge FieldOfKnowledge { get; set; }
    }
}
