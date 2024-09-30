using SecureCitizen.Demo.Core.Helpers;
using SecureCitizen.Demo.Core.Services;
using SecureCitizen.Demo.Presentation.Views;

namespace SecureCitizen.Demo;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        //MainPage = new AppShell();
    }

    public static NavigationService NavigationService { get; set; } = new NavigationService();
    public static LoadingService LoadingService { get; set; } = new LoadingService();
    
    protected override async void OnStart()
    {
        
            await Session.LoadSessionAsync();
            if (!Session.IsAuthenticated())
            {
                MainPage = this.Handler.MauiContext.Services.GetService<LoginPage>();
            }
            else
            {
                MainPage = new AppShell();
            }
        
    }
    
    protected override Window CreateWindow(IActivationState activationState)
    {
        if (this.MainPage == null)
        {
            Task.Run(async () => await Session.LoadSessionAsync()).GetAwaiter().GetResult();

            if (!Session.IsAuthenticated())
            {
                MainPage = this.Handler.MauiContext.Services.GetService<LoginPage>();
            }
            else
            {
                MainPage = new AppShell();

            }
        }

        return base.CreateWindow(activationState);
    }
}