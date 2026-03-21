using ClassesManager.ViewModels;

namespace ClassesManager.Pages;

public partial class SubjectDetailsPage : ContentPage
{
    public SubjectDetailsPage(SubjectDetailsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
