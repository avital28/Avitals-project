using Avitals_project.Services;
using Avitals_project.ViewModels;
using Avitals_project.Views;
using Microsoft.Extensions.Logging;

namespace Avitals_project
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

#if DEBUG
		builder.Logging.AddDebug();
            builder.Services.AddSingleton<UserService>();
            builder.Services.AddSingleton<LoginPage>();
            builder.Services.AddSingleton<LoginPageViewModel>();
            builder.Services.AddSingleton<RegisterPage>();
            builder.Services.AddSingleton<RegisterPageViewModel>();
            builder.Services.AddTransient<WelcomePage>();
            builder.Services.AddTransient<WelcomePageViewModel>();
            

#endif

            return builder.Build();
        }
    }
}