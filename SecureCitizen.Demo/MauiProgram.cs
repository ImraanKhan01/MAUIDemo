using BarcodeScanning;
using Camera.MAUI;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using SecureCitizen.Demo.Data.Interfaces;
using SecureCitizen.Demo.Data.Repositories;
using SecureCitizen.Demo.Data.Services;
using SecureCitizen.Demo.Core.Helpers;
#if ANDROID
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
#endif

namespace SecureCitizen.Demo;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseBarcodeScanning()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            .ConfigureMauiHandlers(handlers =>
            {
#if ANDROID
                Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("NoUnderline", (h, v) =>
                {
                    h.PlatformView.BackgroundTintList =
                   Android.Content.Res.ColorStateList.ValueOf(Colors.Transparent.ToAndroid());
                });
#endif

            })
            .RegisterServices()
            .RegisterViewModels()
            .RegisterViews();
#if DEBUG
        builder.Logging.AddDebug();
#endif
        NavigationHelper.RegisterRoutes();
        return builder.Build();
    }
    
    public partial class CustomEntryHandler
    {
        public static void MapEntryHandlers(IMauiHandlersCollection handlers)
        {
            // Shared configuration (if any)
            ConfigurePlatformSpecificEntry(handlers);
        }

        static partial void ConfigurePlatformSpecificEntry(IMauiHandlersCollection handlers);
    }
    
    private  static MauiAppBuilder  RegisterServices(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<IAuthService, AuthService>();
        mauiAppBuilder.Services.AddSingleton<ITestApiService, TestApiService>();
        mauiAppBuilder.Services.AddSingleton<ITestApiRepository, TestApiRepository>();
        return mauiAppBuilder;
       
    }
    private static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddTransient<Presentation.ViewModels.LoginViewModel>();
        mauiAppBuilder.Services.AddTransient<Presentation.ViewModels.HomeViewModel>();
        mauiAppBuilder.Services.AddTransient<Presentation.ViewModels.ScanViewModel>();
        return mauiAppBuilder;
        
    }
    private static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddTransient<Presentation.Views.LoginPage>();
        mauiAppBuilder.Services.AddTransient<Presentation.Views.HomePage>();
        mauiAppBuilder.Services.AddTransient<Presentation.Views.ScanPage>();
        return mauiAppBuilder;
    }
}