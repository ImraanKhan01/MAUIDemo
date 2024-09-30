using CommunityToolkit.Mvvm.Input;
using SecureCitizen.Demo.Core.Helpers;
using SecureCitizen.Demo.Data.Interfaces;
using SecureCitizen.Demo.Data.Models;
using SecureCitizen.Demo.Presentation.Views;

namespace SecureCitizen.Demo.Presentation.ViewModels;

public class HomeViewModel : BaseViewModel
{
    private ITestApiService _testApiService;
    public HomeViewModel(ITestApiService testApiService)
    {
        this.Name = Session.Username;
        this._testApiService = testApiService;
    }
    
    
    private string name;
    public string Name
    {
        get => this.name;

        set
        {
            if (this.name != value)
            {
                this.name = value;
                OnPropertyChanged(nameof(Name)); // reports this property
            }
        }
    }
    
    private string description;
    public string Description
    {
        get => this.description;

        set
        {
            if (this.description != value)
            {
                this.description = value;
                OnPropertyChanged(nameof(Description)); // reports this property
            }
        }
    }
    
    private List<Claim> claims;
    public List<Claim> Claims
    {
        get => this.claims;

        set
        {
            if (this.claims != value)
            {
                this.claims = value;
                OnPropertyChanged(nameof(Claims)); // reports this property
            }
        }
    }
    
    public IAsyncRelayCommand LogoutCommand
    {
        get
        {
            return new AsyncRelayCommand(LogoutAsync);
        }
    }

    private async Task LogoutAsync()
    {
        await Session.ClearSessionAsync();
        Application.Current.MainPage = Shell.Current.Handler.MauiContext.Services.GetService<LoginPage>();
    }
    public IAsyncRelayCommand GoToScannerCommand
    {
        get
        {
            return new AsyncRelayCommand(GoToScannerPageAsync);
        }
    }

    private async Task GoToScannerPageAsync()
    {
        App.LoadingService.IsLoading = true;
        await App.NavigationService.NavigateTo(NavigationHelper.Routes.Scanner);
        App.LoadingService.IsLoading = false;
        
        
    }
    
    public IAsyncRelayCommand CallApiCommand
    {
        get
        {
            return new AsyncRelayCommand(CallApiAsync);
        }
    }
    
    private async Task CallApiAsync()
    {
        App.LoadingService.IsLoading = true;
        this.Claims =  await this._testApiService.TestGetClaimsAsync();
        if (this.Claims.Count > 0)
        {
            this.Description = string.Join($",{Environment.NewLine} ", this.Claims.Select(c => c.Value.ToString()));
        }
        App.LoadingService.IsLoading = false;
      
        
        
    }
    
    
}