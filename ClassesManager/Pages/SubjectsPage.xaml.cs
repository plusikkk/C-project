using ClassesManager.ViewModels;

namespace ClassesManager.Pages;

public partial class SubjectsPage : ContentPage
{
    private readonly SubjectsViewModel _viewModel;
    // The ViewModel is injected automatically by the IoC container
    public SubjectsPage(SubjectsViewModel viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.LoadSubjectsCommand.Execute(null);
    }
}