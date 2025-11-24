using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LinuxInstaller.ViewModels.Interfaces; // Add this using directive

namespace LinuxInstaller.ViewModels;

public partial class LoadingViewModel : ObservableObject, INavigatableViewModel
{
    public LoadingViewModel()
    {
        // Constructor for LoadingViewModel
    }

    // INavigatableViewModel Implementation
    public bool CanProceed => false; // Cannot proceed during loading
    public bool CanGoBack => false; // Cannot go back during loading
}
