using ClassesManager.ViewModels;
namespace ClassesManager.Pages;

public partial class SubjectEditPage : ContentPage
{
    public SubjectEditPage(SubjectEditViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}