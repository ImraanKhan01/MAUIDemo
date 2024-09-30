using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Camera.MAUI;
using Camera.MAUI.ZXing;
using Camera.MAUI.ZXingHelper;
using SecureCitizen.Demo.Presentation.ViewModels;

namespace SecureCitizen.Demo.Presentation.Views;

public partial class ScanPage : ContentPage
{
    public ScanPage(ScanViewModel viewModel)
    {
        InitializeComponent();
        this.BindingContext = viewModel;
        
    }

    protected override void OnDisappearing()
    {
        scannerControl.DisconnectHandler();
    }
}