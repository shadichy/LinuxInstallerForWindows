using CommunityToolkit.Mvvm.ComponentModel;

namespace LinuxInstaller.Models;

public partial class Distro : ObservableObject
{
    public string DistroName { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public long Size { get; set; }
    public string DownloadUrl { get; set; } = string.Empty;
    public string IconUrl { get; set; } = string.Empty;

    [ObservableProperty]
    private bool _isSelected;
}
