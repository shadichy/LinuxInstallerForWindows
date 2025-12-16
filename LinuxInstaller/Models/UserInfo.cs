using CommunityToolkit.Mvvm.ComponentModel;

namespace LinuxInstaller.Models;

public partial class UserInfo : ObservableObject
{
    [ObservableProperty]
    private string? _fullName = string.Empty;

    [ObservableProperty]
    private string _username = string.Empty;

    [ObservableProperty]
    private string _password = string.Empty;

    [ObservableProperty]
    private string _confirmPassword = string.Empty;

    [ObservableProperty]
    private bool _autoLogin = false;

    [ObservableProperty]
    private bool _requirePasswordOnLogin = true;
}
