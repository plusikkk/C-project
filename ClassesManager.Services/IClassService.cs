using ClassesManager.DTOModels;
using System;

namespace ClassesManager.Services
{
    public interface IClassService
    {
        // returns dto woth details
        ClassDetailsDTO GetClassDetails(Guid classId);
    }
}
