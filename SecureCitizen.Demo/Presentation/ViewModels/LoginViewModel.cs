using CommunityToolkit.Mvvm.Input;
using SecureCitizen.Demo.Data.Interfaces;
using SecureCitizen.Demo.Core.Helpers;
using SecureCitizen.Demo.Data.Models;

namespace SecureCitizen.Demo.Presentation.ViewModels;

public class LoginViewModel : BaseViewModel
{
    private IAuthService _authService;
   
    public LoginViewModel(IAuthService authService)
    {
        this._authService = authService;

    }
 
    public IAsyncRelayCommand LoginCommand
    {
        get
        {
            return new AsyncRelayCommand(AuthenticateUserAsync);
        }
    }

    private async Task AuthenticateUserAsync()
    {
        App.LoadingService.IsLoading = true;
       await this._authService.LoginAsync();
       if (Session.IsAuthenticated())
       {
           await CallApi();
           Application.Current.MainPage = new AppShell();
       }
       else
       {
           
       }
       App.LoadingService.IsLoading = false;



    }

    private async Task CallApi()
    {
       
    }

 
    
    
}