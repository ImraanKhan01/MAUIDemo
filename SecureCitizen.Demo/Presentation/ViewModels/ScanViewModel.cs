

using BarcodeScanning;

namespace SecureCitizen.Demo.Presentation.ViewModels;

public class ScanViewModel: BaseViewModel
{
    public ScanViewModel()
    {
       // this.IsDetecting = true;
    }
    private BarcodeResult[] results;
    public BarcodeResult[] Results
    {
        get => this.results;

        set
        {
            if (this.results != value)
            {
                this.results = value;
                DisplayData();
                OnPropertyChanged(nameof(Results)); // reports this property
            }
        }
    }
    
    private string qrData;
    public string QrData
    {
        get => this.qrData;

        set
        {
            if (this.qrData != value)
            {
                this.qrData = value;
                OnPropertyChanged(nameof(QrData)); // reports this property
            }
        }
    }
    
    private bool isDetecting;
    public bool IsDetecting
    {
        get => this.isDetecting;

        set
        {
            if (this.isDetecting != value)
            {
                this.isDetecting = value;
                OnPropertyChanged(nameof(IsDetecting)); // reports this property
            }
        }
    }

    private void DisplayData()
    {
        if (this.Results.Length > 0)
        {
            this.QrData = this.Results[0]?.DisplayValue ?? "";
        }
    }
}