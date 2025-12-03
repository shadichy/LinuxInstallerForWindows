using CommunityToolkit.Mvvm.ComponentModel;

namespace LinuxInstaller.Models;

public partial class Distro : ObservableObject
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public long Size { get; set; }

    [ObservableProperty]
    private bool _isSelected;
}
