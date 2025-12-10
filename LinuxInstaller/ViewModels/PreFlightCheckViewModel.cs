using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LinuxInstaller.Services;
using LinuxInstaller.ViewModels.Interfaces;
using System.Threading.Tasks; // Add this using directive

namespace LinuxInstaller.ViewModels;

public partial class PreFlightCheckViewModel : NavigatableViewModelBase
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

    public PreFlightCheckViewModel(NavigationService navigationService, SystemAnalysisService systemAnalysisService) : base(navigationService)
    {
        _systemAnalysisService = systemAnalysisService;
        //IsAdmin = _systemAnalysisService.IsRunningAsAdmin();
        //IsUefi = _systemAnalysisService.GetBootMode() == "UEFI";
        //IsSecureBootEnabled = _systemAnalysisService.GetSecureBootStatus(); // TODO: Determine if this should block or warn
        //IsBitLockerEnabled = _systemAnalysisService.GetBitLockerStatus() == 1; // TODO: Determine if this should block or warn
    }

    [RelayCommand]
    private async Task RelaunchAsAdmin()
    {
       await _systemAnalysisService.RelaunchAsAdmin();
    }

    // INavigatableViewModel Implementation
    public override bool CanProceed => IsAdmin && IsUefi; // Critical checks for proceeding
    public override bool CanGoBack => true;
}
