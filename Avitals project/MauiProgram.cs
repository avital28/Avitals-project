using Avitals_project.Services;
using Avitals_project.ViewModels;
using Avitals_project.Views;
using Microsoft.Extensions.Logging;
using SkiaSharp.Views.Maui.Controls.Hosting;
using Syncfusion.Maui.Core.Hosting;
using Syncfusion.Maui.Popup;
using CommunityToolkit.Maui;
namespace Avitals_project
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .UseSkiaSharp()
                .ConfigureSyncfusionCore()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("interfont.ttf", "interfont");
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
            builder.Services.AddTransient<AddAlbumPage>();
            builder.Services.AddTransient<AddAlbumPageViewModel>();
            builder.Services.AddTransient<CreateAlbumPage>();
            builder.Services.AddTransient<CreateAlbumPageViewModel>();


#endif

            return builder.Build();
        }
    }
}