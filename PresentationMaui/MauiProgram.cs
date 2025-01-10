using Microsoft.Extensions.Logging;
using Shared.Interfaces;
using Shared.Services;
using PresentationMaui.Pages;

namespace PresentationMaui
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

            builder.Services.AddSingleton<ICustomerService, CustomerService>();
            builder.Services.AddSingleton<SavedCustomerPage>();
            builder.Services.AddSingleton<EditCustomerPage>();
            builder.Services.AddSingleton<MainPage>();


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}



