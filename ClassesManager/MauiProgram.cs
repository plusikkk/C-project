using ClassesManager.Pages;
using ClassesManager.Repositories;
using ClassesManager.Services;
using ClassesManager.ViewModels;
using Microsoft.Extensions.Logging;

namespace ClassesManager
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddTransient<SubjectsPage>();
            builder.Services.AddSingleton<IStorageRepository, StorageRepository>();
            builder.Services.AddSingleton<ISubjectService, SubjectService>();
            builder.Services.AddSingleton<IClassService, ClassService>();

            builder.Services.AddTransient<SubjectsViewModel>();
            builder.Services.AddTransient<SubjectDetailsViewModel>();
            builder.Services.AddTransient<ClassDetailsViewModel>();

            builder.Services.AddTransient<SubjectDetailsPage>();
            builder.Services.AddTransient<ClassDetailsPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif
            return builder.Build();
        }
    }
}
