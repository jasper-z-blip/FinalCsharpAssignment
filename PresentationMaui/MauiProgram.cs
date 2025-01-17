using Shared.Interfaces;
using Shared.Services;
using Shared.Factories;

namespace PresentationMaui;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        // Som en "låda" där den samlar allt som appen behöver och förbereder för att appen ska fungera. 
        var builder = MauiApp.CreateBuilder(); 
        builder
            .UseMauiApp<App>() // Detta blir Huvudklassen för appen.
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular"); 
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // Dependency injection med en extension jag installera Microsoft.Extension.DependencyInjection, detta gör tjänster tillgängliga för appen. Denpendency Injection har jag förstått löser beoenden automatiskt.
        // Singleton = En instans.
        builder.Services.AddSingleton<IFileService, FileService>();
        builder.Services.AddSingleton<ICustomerService, CustomerService>();
        builder.Services.AddSingleton<CustomerFactory>();
        builder.Services.AddSingleton<ICustomerManagerService, CustomerManagerService>();

        return builder.Build();
    }
}