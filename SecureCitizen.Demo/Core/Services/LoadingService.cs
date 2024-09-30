using System.ComponentModel;
using System.Runtime.CompilerServices;
using Mopups.Services;
using SecureCitizen.Demo.Presentation.CustomControls;
using SecureCitizen.Demo.Presentation.ViewModels;

namespace SecureCitizen.Demo.Core.Services;

    public class LoadingService :  INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool isLoading;

        public bool IsLoading
        {
            get => isLoading;
            set
            {
                if (isLoading != value)
                {
                    isLoading = value;
                    if(isLoading)
                    {
                        MopupService.Instance.PushAsync(new LoadingIndicatorControl());

            
                    }
                    else
                    {
                        MopupService.Instance.PopAsync();
                    }
                    OnPropertyChanged(nameof(IsLoading));
                }
            }
        }
    }
