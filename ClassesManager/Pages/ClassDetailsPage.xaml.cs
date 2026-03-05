using ClassesManager.UIModels;
using System.Data;

namespace ClassesManager.Pages;

public partial class ClassDetailsPage : ContentPage
{
    public ClassDetailsPage(ClassesUIModel classModel)
    {
        InitializeComponent();

        // Filling fields with data from models
        lblTheme.Text = classModel.ThemeOfClass;
        lblType.Text = classModel.TypeOfClass.ToString();
        lblDate.Text = classModel.Date.ToString();

        lblTime.Text = $"{classModel.StartTime} - {classModel.EndTime}";

        lblDuration.Text = $"{classModel.ClassDuration} minutes";
    }
}