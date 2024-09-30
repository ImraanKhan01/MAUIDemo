using BarcodeScanning;

namespace SecureCitizen.Demo.Presentation.CustomControls;

public partial class ScannerControl : ContentView
{
    public ScannerControl()
    {
        Methods.AskForRequiredPermissionAsync();
        InitializeComponent();
        
    }

    public BarcodeResult[] Results
    {
        get => base.GetValue(ResultsProperty) as BarcodeResult[];
        set { base.SetValue(ResultsProperty, value); }
    }

    public static readonly BindableProperty ResultsProperty = BindableProperty.Create(
        propertyName: "Results",
        returnType: typeof(BarcodeResult[]),
        declaringType: typeof(ScannerControl),
        defaultValue: new BarcodeResult[0],
        defaultBindingMode: BindingMode.TwoWay,
        propertyChanged: ResultsPropertyChanged
    );

    private static void ResultsPropertyChanged(
        BindableObject bindable,
        object oldValue,
        object newValue
    )
    {
        var control = (ScannerControl)bindable;
        if (newValue != null)
        {
          
        }
    }
    
    public bool IsDetecting
    {
        get => (bool)base.GetValue(IsDetectingProperty);
        set { base.SetValue(IsDetectingProperty, value); }
    }

    public static readonly BindableProperty IsDetectingProperty = BindableProperty.Create(
        propertyName: "IsDetecting",
        returnType: typeof(bool),
        declaringType: typeof(ScannerControl),
        defaultValue: true,
        defaultBindingMode: BindingMode.TwoWay,
        propertyChanged: IsDetectingPropertyChanged
    );

    private static void IsDetectingPropertyChanged(
        BindableObject bindable,
        object oldValue,
        object newValue
    )
    {
        var control = (ScannerControl)bindable;
        if (newValue != null)
        {
          //control.scanner.CameraEnabled = (bool)newValue;
        }
    }
    public void DisconnectHandler()
    {
        scanner?.Handler?.DisconnectHandler();
    }



    private void CameraView_OnOnDetectionFinished(object? sender, OnDetectionFinishedEventArg e)
    {
        if (e.BarcodeResults.Length > 0)
        {
            this.Results = e.BarcodeResults;
        }
        
       
       // scanner.Handler.DisconnectHandler();
    }
    
}