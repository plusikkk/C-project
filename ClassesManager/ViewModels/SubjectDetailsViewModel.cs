using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ClassesManager.DTOModels;
using ClassesManager.Services;
using ClassesManager.Pages;
using System.Collections.ObjectModel;

namespace ClassesManager.ViewModels
{
    public partial class SubjectDetailsViewModel : ObservableObject
    {
        private readonly ISubjectService _subjectService;
        private readonly IClassService _classService;
        private readonly IServiceProvider _services;

        [ObservableProperty] private SubjectDetailsDTO _subject;
        [ObservableProperty] private ClassListDTO _selectedClass;

        // lists for display and search
        public ObservableCollection<ClassListDTO> Classes { get; } = new();
        public ObservableCollection<ClassListDTO> FilteredClasses { get; } = new();

        [ObservableProperty] private string _searchQuery = string.Empty;
        [ObservableProperty] private string _selectedSortOption = "Date (Oldest first)";

        public List<string> SortOptions { get; } = new()
        {
            "Date (Oldest first)",
            "Date (Newest first)",
            "Theme (A-Z)"
        };

        public SubjectDetailsViewModel(ISubjectService subjectService, IClassService classService, IServiceProvider services)
        {
            _subjectService = subjectService;
            _classService = classService;
            _services = services;
        }

        public async Task LoadDataAsync(Guid subjectId)
        {
            Subject = await _subjectService.GetSubjectDetailsAsync(subjectId);

            Classes.Clear();
            if (Subject?.Classes != null)
            {
                foreach (var c in Subject.Classes)
                    Classes.Add(c);
            }
            ApplyFilters();
        }

        partial void OnSearchQueryChanged(string value) => ApplyFilters();
        partial void OnSelectedSortOptionChanged(string value) => ApplyFilters();

        private void ApplyFilters()
        {
            var filtered = Classes.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(SearchQuery))
            {
                filtered = filtered.Where(c => c.Theme.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase));
            }

            if (SelectedSortOption == "Date (Oldest first)")
                filtered = filtered.OrderBy(c => DateTime.ParseExact(c.Date, "dd.MM.yyyy", null));
            else if (SelectedSortOption == "Date (Newest first)")
                filtered = filtered.OrderByDescending(c => DateTime.ParseExact(c.Date, "dd.MM.yyyy", null));
            else if (SelectedSortOption == "Theme (A-Z)")
                filtered = filtered.OrderBy(c => c.Theme);

            FilteredClasses.Clear();
            foreach (var item in filtered)
                FilteredClasses.Add(item);
        }

        [RelayCommand]
        private async Task GoToClass()
        {
            if (SelectedClass == null) return;
            var classId = SelectedClass.Id;
            SelectedClass = null;

            var detailsPage = _services.GetService<ClassDetailsPage>();
            var viewModel = (ClassDetailsViewModel)detailsPage.BindingContext;
            await viewModel.LoadDataAsync(classId);
            await Application.Current.MainPage.Navigation.PushAsync(detailsPage);
        }

        // COMMANDS
        [RelayCommand]
        private async Task AddClass()
        {
            var editPage = _services.GetService<ClassesCreatePage>();
            var viewModel = (ClassEditViewModel)editPage.BindingContext;
            await viewModel.LoadAsync(Subject.Id);
            await Application.Current.MainPage.Navigation.PushAsync(editPage);
        }

        [RelayCommand]
        private async Task EditClass(ClassListDTO classDto)
        {
            var editPage = _services.GetService<ClassesCreatePage>();
            var viewModel = (ClassEditViewModel)editPage.BindingContext;
            await viewModel.LoadAsync(Subject.Id, classDto);
            await Application.Current.MainPage.Navigation.PushAsync(editPage);
        }

        [RelayCommand]
        private async Task DeleteClass(ClassListDTO classDto)
        {
            if (classDto == null) return;
            bool confirm = await Application.Current.MainPage.DisplayAlert("Delete", $"Delete lesson '{classDto.Theme}'?", "Yes", "No");
            if (confirm)
            {
                await _classService.DeleteClassAsync(classDto.Id);
                await LoadDataAsync(Subject.Id);
            }
        }
    }
}