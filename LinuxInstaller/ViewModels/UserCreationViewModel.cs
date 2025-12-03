using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LinuxInstaller.ViewModels.Interfaces;
using LinuxInstaller.Models; // Add this using directive
using LinuxInstaller.Services; // Add this using directive

namespace LinuxInstaller.ViewModels;

public partial class UserCreationViewModel : ObservableObject, INavigatableViewModel
{
    private readonly InstallationConfigService _installationConfigService;

    [ObservableProperty]
    private UserInfo _user;

    public UserCreationViewModel(InstallationConfigService installationConfigService)
    {
        _installationConfigService = installationConfigService;
        _user = _installationConfigService.UserInfo; // Use the shared UserInfo instance
    }

    // INavigatableViewModel Implementation
    public bool CanProceed =>
        !string.IsNullOrWhiteSpace(User.FullName) &&
        !string.IsNullOrWhiteSpace(User.Username) &&
        !string.IsNullOrWhiteSpace(User.Password) &&
        User.Password == User.ConfirmPassword;

    public bool CanGoBack => true;
}
