using ClassesManager.Services;
using ClassesManager.UIModels;
using System.Collections.ObjectModel;

namespace ClassesManager.Pages;

public partial class SubjectsPage : ContentPage
{
    private readonly IStorageService _storageService;

    // ObservableCollection update the screen
    public ObservableCollection<SubjectUIModel> Subjects { get; set; } = new ObservableCollection<SubjectUIModel>();

    public SubjectsPage(IStorageService storageService)
    {
        InitializeComponent();

        _storageService = storageService;

        cvSubjects.ItemsSource = Subjects;

        LoadSubjects();
    }

    private void LoadSubjects()
    {
        Subjects.Clear();
        var dbSubjects = _storageService.GetAllSubjects();

        foreach (var dbSubject in dbSubjects)
        {
            Subjects.Add(new SubjectUIModel(dbSubject));
        }
    }

    private async void OnSubjectSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is SubjectUIModel selectedSubject)
        {
            ((CollectionView)sender).SelectedItem = null;

            await Navigation.PushAsync(new SubjectDetailsPage(selectedSubject, _storageService));
        }
    }
}