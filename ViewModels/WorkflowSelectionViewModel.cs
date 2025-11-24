using CommunityToolkit.Mvvm.ComponentModel;
using LinuxInstaller.Models;
using LinuxInstaller.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace LinuxInstaller.ViewModels;

public partial class WorkflowSelectionViewModel : ObservableObject
{
    public ObservableCollection<Distro> Distros { get; } = new();

    public WorkflowSelectionViewModel()
    {
        _ = LoadDistrosAsync();
    }

    private async Task LoadDistrosAsync()
    {
        var distroService = new DistroService("https://raw.githubusercontent.com/your-username/your-repo/main/prebuilt/distros.json");
        var distros = await distroService.GetDistrosAsync();
        foreach (var distro in distros)
        {
            Distros.Add(distro);
        }
    }
}


