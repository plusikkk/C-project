using ClassesManager.CommonComponents.Enums;
using ClassesManager.DTOModels;
using ClassesManager.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;


namespace ClassesManager.ViewModels
{
    public partial class ClassEditViewModel : ObservableObject
    {
        private readonly IClassService _classService;
        private Guid _subjectId;
        private ClassListDTO _originalClass;

        [ObservableProperty] private string _title;
        [ObservableProperty] private string _theme;
        [ObservableProperty] private TypeOfClass _selectedType;
        [ObservableProperty] private DateTime _date;
        [ObservableProperty] private TimeSpan _startTime;
        [ObservableProperty] private TimeSpan _endTime;

        public List<TypeOfClass> Types { get; } = Enum.GetValues<TypeOfClass>().Cast<TypeOfClass>().ToList();

        public ClassEditViewModel(IClassService classService)
        {
            _classService = classService;
        }

        public async Task LoadAsync(Guid subjectId, ClassListDTO classDto = null)
        {
            _subjectId = subjectId;
            _originalClass = classDto;

            if (classDto == null)
            {
                Title = "New Lesson";
                Theme = string.Empty;
                SelectedType = Types.FirstOrDefault();
                Date = DateTime.Today;
                StartTime = new TimeSpan(8, 30, 0);
                EndTime = new TimeSpan(10, 5, 0);
            }
            else
            {
                Title = "Edit Lesson";
                // load details to get time
                var details = await _classService.GetClassDetailsAsync(classDto.Id);

                Theme = details.Theme;
                SelectedType = Enum.Parse<TypeOfClass>(details.Type);
                Date = DateTime.ParseExact(details.Date, "dd.MM.yyyy", null);

                var times = details.TimeRange.Split(" - ");
                StartTime = TimeSpan.Parse(times[0]);
                EndTime = TimeSpan.Parse(times[1]);
            }
        }

        [RelayCommand]
        private async Task Save()
        {
            if (string.IsNullOrWhiteSpace(Theme))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Theme is required", "OK");
                return;
            }

            if (_originalClass == null)
            {
                await _classService.AddClassAsync(_subjectId, Theme, (int)SelectedType, Date, StartTime, EndTime);
            }
            else
            {
                await _classService.UpdateClassAsync(_originalClass.Id, Theme, (int)SelectedType, Date, StartTime, EndTime);
            }

            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}