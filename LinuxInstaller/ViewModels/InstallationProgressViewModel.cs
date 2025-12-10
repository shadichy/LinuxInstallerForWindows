using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LinuxInstaller.Services;
using LinuxInstaller.ViewModels.Interfaces; // Add this using directive

namespace LinuxInstaller.ViewModels;

public partial class InstallationProgressViewModel : NavigatableViewModelBase
{
    public InstallationProgressViewModel(NavigationService navigationService) : base(navigationService)
    {
        // Constructor for InstallationProgressViewModel
    }

    // INavigatableViewModel Implementation
    public override bool CanProceed => false; // Cannot proceed during installation
    public override bool CanGoBack => false; // Cannot go back during installation
}
