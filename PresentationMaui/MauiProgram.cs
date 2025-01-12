using Microsoft.Maui;
using Microsoft.Maui.Hosting;
using Shared.Interfaces;
using Shared.Services;
using Shared.Models;
using Shared.Factories;

namespace PresentationMaui;

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

        // Dependency injection

        builder.Services.AddSingleton<IFileService, FileService>();
        builder.Services.AddSingleton<ICustomerService, CustomerService>();
        builder.Services.AddSingleton<CustomerFactory>();



        return builder.Build();
    }
}



