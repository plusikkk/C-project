using ClassesManager.CommonComponents.Enums;
namespace ClassesManager.Pages;

public partial class ClassesCreatePage : ContentPage
{
    public ClassesCreatePage()
    {
        InitializeComponent();
        pTypeOfLesson.ItemsSource = Enum.GetValues<TypeOfClass>();
        // Наразі сторінка створення не використовується в архітектурі 3-ї лаби, 
        // тому залишаємо конструктор безпечним.
    }

    private void CreateClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(eThemeOfLesson.Text))
        {
            DisplayAlert("Incomplete data", "Theme of lesson can't be empty", "Cancel");
            return;
        }

        if (pSubject.SelectedItem == null)
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
        // Логіка для кнопки скасування
    }
}