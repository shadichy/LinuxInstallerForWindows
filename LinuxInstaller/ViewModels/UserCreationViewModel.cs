using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LinuxInstaller.ViewModels.Interfaces;
using LinuxInstaller.Models; // Add this using directive
using LinuxInstaller.Services; // Add this using directive

namespace LinuxInstaller.ViewModels;

public partial class UserCreationViewModel : NavigatableViewModelBase
{
    private readonly InstallationConfigService _installationConfigService;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CanProceed))]
    private UserInfo _user;

    public UserCreationViewModel(NavigationService navigationService, InstallationConfigService installationConfigService) : base(navigationService)
    {
        _installationConfigService = installationConfigService;
        _user = _installationConfigService.UserInfo; // Use the shared UserInfo instance
        _user.PropertyChanged += OnUserPropertyChanged;
    }

    private void OnUserPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        OnPropertyChanged(nameof(CanProceed));
    }

    // INavigatableViewModel Implementation
    public override bool CanProceed =>
        !string.IsNullOrWhiteSpace(User.Username) &&
        !string.IsNullOrWhiteSpace(User.Password) &&
        User.Password == User.ConfirmPassword;

    public override bool CanGoBack => true;

    [RelayCommand]
    private void Next()
    {
        Navigation.Next();
    }

    [RelayCommand]
    private void Back()
    {
        Navigation.Previous();
    }
}
