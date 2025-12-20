using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LinuxInstaller.Services;
using LinuxInstaller.ViewModels.Interfaces;
using System;
using System.Threading.Tasks;

namespace LinuxInstaller.ViewModels;

public partial class InstallationProgressViewModel : NavigatableViewModelBase
{
    [ObservableProperty]
    private string _statusText = "Starting installation...";

    public InstallationProgressViewModel(NavigationService navigationService) : base(navigationService)
    {
    }

    public async void StartInstallationSimulation()
    {
        await Task.Delay(1000);
        StatusText = "Copying files...";
        await Task.Delay(2000);
        StatusText = "Installing bootloader...";
        await Task.Delay(2000);
        StatusText = "Finalizing installation...";
        await Task.Delay(1000);
        Navigation.Next();
    }

    public override bool CanProceed => false;
    public override bool CanGoBack => false;
}


