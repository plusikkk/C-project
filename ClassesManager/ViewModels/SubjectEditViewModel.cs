using ClassesManager.CommonComponents.Enums;
using ClassesManager.DTOModels;
using ClassesManager.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Xml.Linq;

namespace ClassesManager.ViewModels
{
    public partial class SubjectEditViewModel : ObservableObject
    {
        private readonly ISubjectService _subjectService;
        private SubjectListDTO _originalSubject;

        [ObservableProperty] private string _title;
        [ObservableProperty] private string _name;
        [ObservableProperty] private float _ects;
        [ObservableProperty] private FieldOfKnowledge _selectedField;

        public List<FieldOfKnowledge> Fields { get; } = Enum.GetValues<FieldOfKnowledge>().Cast<FieldOfKnowledge>().ToList();

        public SubjectEditViewModel(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        // Load data to update or create
        public void Load(SubjectListDTO subject = null)
        {
            _originalSubject = subject;
            if (subject == null)
            {
                Title = "New Subject";
                Name = string.Empty;
                Ects = 0;
                SelectedField = Fields.FirstOrDefault();
            }
            else
            {
                Title = "Edit Subject";
                Name = subject.Name;
                Ects = subject.ECTS;
                SelectedField = Enum.Parse<FieldOfKnowledge>(subject.FieldOfKnowledge);
            }
        }

        [RelayCommand]
        private async Task Save()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Name is required", "OK");
                return;
            }

            var dto = new SubjectListDTO
            {
                Id = _originalSubject?.Id ?? Guid.Empty,
                Name = Name,
                ECTS = Ects,
                FieldOfKnowledge = SelectedField.ToString()
            };

            if (_originalSubject == null)
                await _subjectService.AddSubjectAsync(dto);
            else
                await _subjectService.UpdateSubjectAsync(dto);

            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
