using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LinuxInstaller.ViewModels.Interfaces; // Add this using directive

namespace LinuxInstaller.ViewModels;

public partial class InstallationProgressViewModel : ObservableObject, INavigatableViewModel
{
    public InstallationProgressViewModel()
    {
        // Constructor for InstallationProgressViewModel
    }

    // INavigatableViewModel Implementation
    public bool CanProceed => false; // Cannot proceed during installation
    public bool CanGoBack => false; // Cannot go back during installation
}
