using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassesManager.DTOModels
{
    public class SubjectListDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public float ECTS { get; set; }
        public string FieldOfKnowledge { get; set; }
    }
}
