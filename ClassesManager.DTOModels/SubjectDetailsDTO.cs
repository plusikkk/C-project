using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassesManager.DTOModels
{
    public class SubjectDetailsDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public float ECTS { get; set; }
        public string FieldOfKnowledge { get; set; }
        public int TotalDuration { get; set; }
        public List<ClassListDTO> Classes { get; set; } = new();
    }
}
