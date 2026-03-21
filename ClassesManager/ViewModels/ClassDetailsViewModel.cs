using CommunityToolkit.Mvvm.ComponentModel;
using ClassesManager.DTOModels;
using ClassesManager.Services;

namespace ClassesManager.ViewModels
{
    public partial class ClassDetailsViewModel : ObservableObject
    {
        private readonly IClassService _classService;

        // Holds the full details of the specific class
        [ObservableProperty]
        private ClassDetailsDTO _classDetails;

        public ClassDetailsViewModel(IClassService classService)
        {
            _classService = classService;
        }

        // Loads data from the service using the provided ID
        public void LoadData(Guid classId)
        {
            ClassDetails = _classService.GetClassDetails(classId);
        }
    }
}