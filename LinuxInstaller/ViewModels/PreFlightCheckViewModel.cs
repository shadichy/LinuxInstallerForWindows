using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LinuxInstaller.Services;
using LinuxInstaller.ViewModels.Interfaces; // Add this using directive

namespace LinuxInstaller.ViewModels;

public partial class PreFlightCheckViewModel : ObservableObject, INavigatableViewModel
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

    public PreFlightCheckViewModel(SystemAnalysisService systemAnalysisService)
    {
        _systemAnalysisService = systemAnalysisService;
        IsAdmin = _systemAnalysisService.IsRunningAsAdmin();
        IsUefi = _systemAnalysisService.GetBootMode() == "UEFI";
        IsSecureBootEnabled = _systemAnalysisService.GetSecureBootStatus(); // TODO: Determine if this should block or warn
        IsBitLockerEnabled = _systemAnalysisService.GetBitLockerStatus() == 1; // TODO: Determine if this should block or warn
    }

    [RelayCommand]
    private void RelaunchAsAdmin()
    {
        _systemAnalysisService.RelaunchAsAdmin();
    }

    // INavigatableViewModel Implementation
    public bool CanProceed => IsAdmin && IsUefi; // Critical checks for proceeding
    public bool CanGoBack => true;
}
