using SecureCitizen.Demo.Presentation.Views;

namespace SecureCitizen.Demo.Core.Helpers;

public static class NavigationHelper
{
    public enum Routes
    {
        Home,
        Login,
        Scanner
    }

    public static void RegisterRoutes()
    {
        Routing.RegisterRoute(nameof(ScanPage), typeof(ScanPage));
        Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
    }

    public static string GetRouteURL(Routes routeName)
    {
        if (routeName == Routes.Scanner)
        {
            return nameof(ScanPage);
        }
        else if (routeName == Routes.Login)
        {
            return nameof(LoginPage);
        }
        else if (routeName == Routes.Home)
        {
            return $"///{nameof(HomePage)}";
        }

        return "";
    }
}