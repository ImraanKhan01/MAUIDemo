using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecureCitizen.Demo.Presentation.ViewModels;

namespace SecureCitizen.Demo.Presentation.Views;

public partial class HomePage : ContentPage
{
    public HomePage(HomeViewModel viewModel)
    {
        InitializeComponent();
        this.BindingContext = viewModel;
    }
}