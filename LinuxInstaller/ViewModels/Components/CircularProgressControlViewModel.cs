using CommunityToolkit.Mvvm.ComponentModel;

namespace LinuxInstaller.ViewModels.Components
{
    public partial class CircularProgressControlViewModel : ObservableObject
    {
        [ObservableProperty]
        private double _value;

        [ObservableProperty]
        private bool _isIndeterminate;

        [ObservableProperty]
        private double _strokeThickness = 4.0;
    }
}
