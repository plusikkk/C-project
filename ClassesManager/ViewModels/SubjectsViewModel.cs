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
        private readonly ISubjectService _subjectService;
        private readonly IServiceProvider _services;

        // original list 
        public ObservableCollection<SubjectListDTO> Subjects { get; } = new();

        // list with filters
        public ObservableCollection<SubjectListDTO> FilteredSubjects { get; } = new();

        [ObservableProperty] private SubjectListDTO _selectedSubject;

        // fields for searching and filters
        [ObservableProperty] private string _searchQuery = string.Empty;
        [ObservableProperty] private string _selectedSortOption = "Name (A-Z)";

        public List<string> SortOptions { get; } = new()
        {
            "Name (A-Z)",
            "ECTS (Low to High)",
            "ECTS (High to Low)"
        };

        public SubjectsViewModel(ISubjectService subjectService, IServiceProvider services)
        {
            _subjectService = subjectService;
            _services = services;
            LoadSubjectsCommand.Execute(null);
        }

        [RelayCommand]
        public async Task LoadSubjects()
        {
            var data = await _subjectService.GetSubjectsListAsync();
            Subjects.Clear();
            foreach (var item in data)
            {
                Subjects.Add(item);
            }
            ApplyFilters();
        }

        partial void OnSearchQueryChanged(string value) => ApplyFilters();
        partial void OnSelectedSortOptionChanged(string value) => ApplyFilters();

        private void ApplyFilters()
        {
            var filtered = Subjects.AsEnumerable();

            // searching
            if (!string.IsNullOrWhiteSpace(SearchQuery))
            {
                filtered = filtered.Where(s => s.Name.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase));
            }

            // sorting
            if (SelectedSortOption == "Name (A-Z)")
                filtered = filtered.OrderBy(s => s.Name);
            else if (SelectedSortOption == "ECTS (Low to High)")
                filtered = filtered.OrderBy(s => s.ECTS);
            else if (SelectedSortOption == "ECTS (High to Low)")
                filtered = filtered.OrderByDescending(s => s.ECTS);

            // updating list
            FilteredSubjects.Clear();
            foreach (var item in filtered)
            {
                FilteredSubjects.Add(item);
            }
        }

        // COMMANDS
        [RelayCommand]
        private async Task GoToDetails()
        {
            if (SelectedSubject == null) return;
            var selectedId = SelectedSubject.Id;
            SelectedSubject = null;
            var detailsPage = _services.GetService<SubjectDetailsPage>();
            var viewModel = (SubjectDetailsViewModel)detailsPage.BindingContext;
            await viewModel.LoadDataAsync(selectedId);
            await Application.Current.MainPage.Navigation.PushAsync(detailsPage);
        }

        [RelayCommand]
        private async Task AddSubject()
        {
            var editPage = _services.GetService<SubjectEditPage>();
            ((SubjectEditViewModel)editPage.BindingContext).Load();
            await Application.Current.MainPage.Navigation.PushAsync(editPage);
        }

        [RelayCommand]
        private async Task EditSubject(SubjectListDTO subject)
        {
            var editPage = _services.GetService<SubjectEditPage>();
            ((SubjectEditViewModel)editPage.BindingContext).Load(subject);
            await Application.Current.MainPage.Navigation.PushAsync(editPage);
        }

        [RelayCommand]
        private async Task DeleteSubject(SubjectListDTO subject)
        {
            if (subject == null) return;
            bool confirm = await Application.Current.MainPage.DisplayAlert("Delete", $"Are you sure you want to delete '{subject.Name}'?", "Yes", "No");
            if (confirm)
            {
                await _subjectService.DeleteSubjectAsync(subject.Id);
                Subjects.Remove(subject);
                ApplyFilters();
            }
        }
    }
}