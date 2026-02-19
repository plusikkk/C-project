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
        public Guid Id { get; } //Id is generated only once and cannot be changed
        public string Name { get; set; }
        public float ECTS { get; set; }
        public FieldOfKnowledge FieldOfKnowledge { get; set; }

        private SubjectDBModel() 
        {
            
        }

        public SubjectDBModel(string name, float ects, FieldOfKnowledge fieldOfKnowladge) 
        {
            Id = Guid.NewGuid();
            Name = name;
            ECTS = ects;
            FieldOfKnowledge = fieldOfKnowladge;
        }
    }
}
