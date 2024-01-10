using Avitals_project.Services;
using Avitals_project.ViewModels;
using Avitals_project.Views;
using Microsoft.Extensions.Logging;
using Syncfusion.Maui.Core.Hosting;
namespace Avitals_project
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureSyncfusionCore()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
		builder.Logging.AddDebug();
            builder.Services.AddSingleton<UserService>();
            builder.Services.AddSingleton<LoginPage>();
            builder.Services.AddSingleton<LoginPageViewModel>();
            builder.Services.AddSingleton<RegisterPage>();
            builder.Services.AddSingleton<RegisterPageViewModel>();
            builder.Services.AddTransient<WelcomePage>();
            builder.Services.AddTransient<WelcomePageViewModel>();
            builder.Services.AddTransient<UserDetailsPage>(); 
            builder.Services.AddTransient<UserDetailsPageViewModel>();
            builder.Services.AddTransient<SettingsPage>();
            

#endif

            return builder.Build();
        }
    }
}