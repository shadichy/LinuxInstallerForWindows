using CommunityToolkit.Mvvm.ComponentModel;

namespace LinuxInstaller.ViewModels.Components
{
    public partial class ProgressBarControlViewModel : ObservableObject
    {
        [ObservableProperty]
        private double _value;

        [ObservableProperty]
        private bool _isIndeterminate;
    }
}
