using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LinuxInstaller.Services;

namespace LinuxInstaller.ViewModels;

public partial class PreFlightCheckViewModel : ObservableObject
{
    private readonly SystemAnalysisService _systemAnalysisService;

    [ObservableProperty]
    private bool _isAdmin;

    [ObservableProperty]
    private bool _isUefi;

    [ObservableProperty]
    private bool _isSecureBootEnabled;

    [ObservableProperty]
    private bool _isBitLockerEnabled;

    public PreFlightCheckViewModel()
    {
        _systemAnalysisService = new SystemAnalysisService();
        IsAdmin = _systemAnalysisService.IsRunningAsAdmin();
        IsUefi = _systemAnalysisService.GetBootMode() == "UEFI";
        IsSecureBootEnabled = _systemAnalysisService.GetSecureBootStatus();
        IsBitLockerEnabled = _systemAnalysisService.GetBitLockerStatus() == 1;
    }

    [RelayCommand]
    private void RelaunchAsAdmin()
    {
        _systemAnalysisService.RelaunchAsAdmin();
    }
}
