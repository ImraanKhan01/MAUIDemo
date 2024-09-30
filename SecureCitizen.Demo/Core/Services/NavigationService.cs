using SecureCitizen.Demo.Core.Helpers;

namespace SecureCitizen.Demo.Core.Services;

public class NavigationService
{
    public async Task NavigateBack(Dictionary<string, object> navigationParameters = null)
    {
        if (navigationParameters == null)
        {


            await Shell.Current.GoToAsync("..");
        }
        else
        {
            await Shell.Current.GoToAsync("..",navigationParameters);
        }
           
    }

    public async Task NavigateTo(NavigationHelper.Routes route,Dictionary<string, object> navigationParameters = null)
    {

        if(navigationParameters == null)
        {
                

            await Shell.Current.GoToAsync(NavigationHelper.GetRouteURL(route));
        }
        else
        {
            await Shell.Current.GoToAsync(NavigationHelper.GetRouteURL(route), navigationParameters);
        }
    }

    public async Task NavigateTo(string pageName, Dictionary<string, object> navigationParameters = null)
    {

        if (navigationParameters == null)
        {


            await Shell.Current.GoToAsync(pageName);
        }
        else
        {
            await Shell.Current.GoToAsync(pageName, navigationParameters);
        }
    }

    public async Task PopToRoot()
    {
        await Shell.Current.Navigation.PopToRootAsync();
    }
}