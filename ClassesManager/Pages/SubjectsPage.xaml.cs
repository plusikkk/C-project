using ClassesManager.ViewModels;

namespace ClassesManager.Pages;

public partial class SubjectsPage : ContentPage
{
    // The ViewModel is injected automatically by the IoC container
    public SubjectsPage(SubjectsViewModel viewModel)
    {
        InitializeComponent();

        // Setting the DataContext for MVVM bindings in XAML
        BindingContext = viewModel;
    }
}