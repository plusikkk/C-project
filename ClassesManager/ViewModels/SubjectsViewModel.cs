using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ClassesManager.DTOModels;
using ClassesManager.Services;
using System.Collections.ObjectModel;
using ClassesManager.Pages;

namespace ClassesManager.ViewModels
{
    public partial class SubjectsViewModel : ObservableObject
    {
        // Services injected via Dependency Injection (IoC container)
        private readonly ISubjectService _subjectService;
        private readonly IServiceProvider _services;

        // Observable collection to automatically update the UI when items are added/removed
        public ObservableCollection<SubjectListDTO> Subjects { get; } = new();
 
        // [ObservableProperty] automatically generates INotifyPropertyChanged logic.
        [ObservableProperty]
        private SubjectListDTO _selectedSubject;

        // Constructor with injected services
        public SubjectsViewModel(ISubjectService subjectService, IServiceProvider services)
        {
            _subjectService = subjectService;
            _services = services;
            LoadSubjects(); // Initializing data when ViewModel is created
        }

        // Fetches data from the service layer and populates the collection
        private void LoadSubjects()
        {
            var data = _subjectService.GetSubjectsList();
            Subjects.Clear();
            foreach (var item in data)
            {
                Subjects.Add(item);
            }
        }

        // [RelayCommand] automatically generates an ICommand property named "GoToDetailsCommand".
        [RelayCommand]
        private async Task GoToDetails()
        {
            // Ignore if nothing is selected
            if (SelectedSubject == null) return;

            var selectedId = SelectedSubject.Id;

            // Clear selection so the item doesn't remain highlighted upon returning
            SelectedSubject = null;

            // Resolve the target page from the IoC container
            var detailsPage = _services.GetService<SubjectDetailsPage>();

            // Get the ViewModel of the target page and load the specific subject data
            var viewModel = (SubjectDetailsViewModel)detailsPage.BindingContext;
            viewModel.LoadData(selectedId);

            // Perform navigation to the details page
            await Application.Current.MainPage.Navigation.PushAsync(detailsPage);
        }
    }
}