using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LinuxInstaller.Services;
using LinuxInstaller.ViewModels.Interfaces;

namespace LinuxInstaller.ViewModels;

public partial class InstallationFinishViewModel : NavigatableViewModelBase
{
    [ObservableProperty]
    private bool _restartNow = true;

    public InstallationFinishViewModel(NavigationService navigationService) : base(navigationService)
    {
    }

    [RelayCommand]
    private void Finish()
    {
        // TODO: Add logic to handle restart or finish
        // For now, it just closes the app
        System.Environment.Exit(0);
    }

    public override bool CanProceed => true;
    public override bool CanGoBack => false;
}
