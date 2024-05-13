using Avitals_project.Services;
using Avitals_project.ViewModels;
using Avitals_project.Views;
using Microsoft.Extensions.Logging;
using SkiaSharp.Views.Maui.Controls.Hosting;
using Syncfusion.Maui.Core.Hosting;
using Syncfusion.Maui.Popup;
using CommunityToolkit.Maui;
using Xe.AcrylicView;
using Sharpnado.MaterialFrame;

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
                .UseMauiCommunityToolkitMediaElement()
                .UseSkiaSharp()
                .UseAcrylicView()
                .UseSharpnadoMaterialFrame(loggerEnable: false)
                .ConfigureSyncfusionCore()
                
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Kendofonticons.ttf", "FontIcons");
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
            builder.Services.AddTransient<DisplayAlbumsPage>();
            builder.Services.AddTransient<DisplayAlbumsPageViewModel>();    
            builder.Services.AddTransient<AlbumMediaPage>();
            builder.Services.AddTransient<AlbumMediaPageViewModel>();
            builder.Services.AddTransient<ViewAlbumDetailsPage>();
            builder.Services.AddTransient<ViewAlbumDetailsPageViewModel>();
            builder.Services.AddTransient<ShowAllAlbums>();
            builder.Services.AddTransient<ShowAllAlbumsViewModel>();


#endif

            return builder.Build();
        }
    }
}