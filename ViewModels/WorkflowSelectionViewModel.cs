using CommunityToolkit.Mvvm.ComponentModel;
using LinuxInstaller.Models;
using LinuxInstaller.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using LinuxInstaller.ViewModels.Interfaces; // Add this using directive

namespace LinuxInstaller.ViewModels;

public partial class WorkflowSelectionViewModel : ObservableObject, INavigatableViewModel
{
    private readonly DistroService _distroService;

    [ObservableProperty]
    private Distro? _selectedDistro;

    public ObservableCollection<Distro> Distros { get; } = new();

    public WorkflowSelectionViewModel(DistroService distroService)
    {
        _distroService = distroService;
        _ = LoadDistrosAsync();
    }

    private async Task LoadDistrosAsync()
    {
        var distros = await _distroService.GetDistrosAsync();
        foreach (var distro in distros)
        {
            Distros.Add(distro);
        }
    }

    // INavigatableViewModel Implementation
    public bool CanProceed => true; // For now, assume always can proceed
    public bool CanGoBack => true; // For now, assume always can go back
}



