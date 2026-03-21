using ClassesManager.ViewModels;

namespace ClassesManager.Pages;

public partial class ClassDetailsPage : ContentPage
{
    // The UI simply binds to the injected ViewModel properties
    public ClassDetailsPage(ClassDetailsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}