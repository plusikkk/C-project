using ClassesManager.ViewModels;

namespace ClassesManager.Pages;

public partial class SubjectDetailsPage : ContentPage
{
    private readonly SubjectDetailsViewModel _viewModel;

    public SubjectDetailsPage(SubjectDetailsViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (_viewModel.Subject != null)
        {
            await _viewModel.LoadDataAsync(_viewModel.Subject.Id);
        }
    }
}