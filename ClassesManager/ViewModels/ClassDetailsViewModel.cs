using CommunityToolkit.Mvvm.ComponentModel;
using ClassesManager.DTOModels;
using ClassesManager.Services;

namespace ClassesManager.ViewModels
{
    public partial class ClassDetailsViewModel : ObservableObject
    {
        private readonly IClassService _classService;

        [ObservableProperty]
        private ClassDetailsDTO _classDetails;

        public ClassDetailsViewModel(IClassService classService)
        {
            _classService = classService;
        }

        // async method
        public async Task LoadDataAsync(Guid classId)
        {
            ClassDetails = await _classService.GetClassDetailsAsync(classId);
        }
    }
}