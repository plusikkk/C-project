using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ClassesManager.DTOModels;
using ClassesManager.Services;
using ClassesManager.Pages;

namespace ClassesManager.ViewModels
{
    public partial class SubjectDetailsViewModel : ObservableObject
    {
        private readonly ISubjectService _subjectService;
        private readonly IServiceProvider _services;

        // Holds the full details of the subject, including its nested classes
        [ObservableProperty]
        private SubjectDetailsDTO _subject;

        // Tracks the selected child entity (class/lesson) from the UI list
        [ObservableProperty]
        private ClassListDTO _selectedClass;

        public SubjectDetailsViewModel(ISubjectService subjectService, IServiceProvider services)
        {
            _subjectService = subjectService;
            _services = services;
        }

        // Called from the previous page to fetch the required details before navigation
        public void LoadData(Guid subjectId)
        {
            Subject = _subjectService.GetSubjectDetails(subjectId);
        }

        // Command to navigate to the 3rd level (Specific class details)
        [RelayCommand]
        private async Task GoToClass()
        {
            if (SelectedClass == null) return;

            var classId = SelectedClass.Id;
            SelectedClass = null; // Reset selection

            // Resolve the target page
            var detailsPage = _services.GetService<ClassDetailsPage>();
            var viewModel = (ClassDetailsViewModel)detailsPage.BindingContext;

            // Load data for the selected class
            viewModel.LoadData(classId);

            // Navigate deeper
            await Application.Current.MainPage.Navigation.PushAsync(detailsPage);
        }
    }
}