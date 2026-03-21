using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassesManager.DTOModels
{
    public class ClassListDTO
    {
        public Guid Id { get; set;}
        public string Theme { get; set; }
        public string Date { get; set; }
        public string Type { get; set; }
    }
}
