using ClassesManager.Services;
using ClassesManager.UIModels;
using System.Collections.ObjectModel;

namespace ClassesManager.Pages;

public partial class SubjectDetailsPage : ContentPage
{
    private readonly IStorageService _storageService;
    private readonly SubjectUIModel _subject;

    // List to show lessons
    public ObservableCollection<ClassesUIModel> Classes { get; set; } = new ObservableCollection<ClassesUIModel>();

    public SubjectDetailsPage(SubjectUIModel subject, IStorageService storageService)
    {
        InitializeComponent();

        _subject = subject;
        _storageService = storageService;

        // subject info
        lblSubjectName.Text = _subject.Name;
        lblFieldOfKnowledge.Text = $"Field of knowledge: {_subject.FieldOfKnowledge}";
        lblEcts.Text = $"ECTS: {_subject.ECTS}";

        cvClasses.ItemsSource = Classes;

        // Loading subject
        LoadClasses();
    }

    private void LoadClasses()
    {
        if (_subject.Classes.Count == 0 && _subject.Id.HasValue)
        {
            var classesDBs = _storageService.GetAllClasses(_subject.Id.Value);
            foreach (var classDb in classesDBs)
            {
                _subject.AddClass(new ClassesUIModel(classDb));
            }
        }

        Classes.Clear();
        foreach (var cls in _subject.Classes)
        {
            Classes.Add(cls);
        }

        lblTotalDuration.Text = $"Total duration of all lessons: {_subject.AllClassesDuration} minutes.";
    }

    private async void OnClassSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is ClassesUIModel selectedClass)
        {
            ((CollectionView)sender).SelectedItem = null;

            await Navigation.PushAsync(new ClassDetailsPage(selectedClass));
        }
    }
}
