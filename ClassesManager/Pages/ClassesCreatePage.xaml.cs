using ClassesManager.CommonComponents.Enums;
using System.Runtime.InteropServices.JavaScript;
using ClassesManager.Services;
namespace ClassesManager.Pages;

public partial class ClassesCreatePage : ContentPage
{
    private readonly IStorageService _storageService;
    public ClassesCreatePage(IStorageService storageService)
	{
		InitializeComponent();
        _storageService = storageService;
        pTypeOfLesson.ItemsSource = Enum.GetValues<TypeOfClass>();
        pSubject.ItemsSource = _storageService.GetAllSubjects().ToList();
    }

	private void CreateClicked(object sender, EventArgs e) 
	{
		if (String.IsNullOrWhiteSpace(eThemeOfLesson.Text))
		{
			DisplayAlert("Incomplete data", "Theme of lesson can't be empty", "Cancel");
            return;
		}
		if (pSubject.SelectedItem == null)
		{
            DisplayAlert("Incomplete data", "Subject must be selected", "Cancel");
            return;
        }
        if (dDate.Date == null)
        {
            DisplayAlert("Incomplete data", "Date must be selected", "Cancel");
            return;
        }
        if (tStartTime.Time == null || tEndTime.Time == null)
        {
            DisplayAlert("Incomplete data", "Subject must be selected", "Cancel");
            return;
        }
        if (pTypeOfLesson.SelectedItem == null)
        {
            DisplayAlert("Incomplete data", "Type of lesson must be selected", "Cancel");
            return;
        }
    }

    private void CancelClicked(object sender, EventArgs e)
    {

    }
}