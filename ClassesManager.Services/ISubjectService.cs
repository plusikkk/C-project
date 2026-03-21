using ClassesManager.DTOModels;

namespace ClassesManager.Services
{
    public interface ISubjectService
    {
        // returns dto for list
        IEnumerable<SubjectListDTO> GetSubjectsList();

        // returns detail dto
        SubjectDetailsDTO GetSubjectDetails(Guid id);
    }
}