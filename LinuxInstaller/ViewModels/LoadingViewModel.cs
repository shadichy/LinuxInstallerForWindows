using CommunityToolkit.Mvvm.ComponentModel;
using LinuxInstaller.Services;
using LinuxInstaller.ViewModels.Interfaces;
using System.Threading.Tasks;

namespace LinuxInstaller.ViewModels;

public partial class LoadingViewModel : NavigatableViewModelBase
{
    private readonly SystemAnalysisService _systemAnalysisService;

    private string _statusText = "Loading System Information...";
    public string StatusText
    {
        get => _statusText;
        set => SetProperty(ref _statusText, value);
    }

    private bool _canProceed = false;

    public override bool CanProceed => _canProceed;

    public override bool CanGoBack => false;

    public LoadingViewModel(NavigationService navigationService, SystemAnalysisService systemAnalysisService) : base(navigationService)
    {
        _systemAnalysisService = systemAnalysisService;
        _ = PerformSystemChecks();
    }

    private async Task PerformSystemChecks()
    {
        StatusText = "Checking for administrator privileges...";
        await Task.Delay(1000); // Placeholder for real work
        await _systemAnalysisService.IsRunningAsAdmin();

        StatusText = "Checking boot mode...";
        await Task.Delay(1000); // Placeholder for real work
        await _systemAnalysisService.GetBootMode();

        StatusText = "Checking Secure Boot status...";
        await Task.Delay(1000); // Placeholder for real work
        await _systemAnalysisService.GetSecureBootStatus();

        StatusText = "Checking BitLocker status...";
        await Task.Delay(1000); // Placeholder for real work
        await _systemAnalysisService.GetBitLockerStatus();
        
        StatusText = "Done!";
        await Task.Delay(1000);
        
        _canProceed = true;
        Navigation.Next();
    }
}

