using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassesManager.DTOModels
{
    public class ClassDetailsDTO
    {
        public Guid Id { get; set; }
        public string Theme { get; set; }
        public string Type { get; set; }
        public string Date { get; set; }
        public string TimeRange { get; set; }
        public int DurationMinutes { get; set; }
    }
}
