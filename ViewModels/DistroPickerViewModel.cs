using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LinuxInstaller.Models;
using LinuxInstaller.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using LinuxInstaller.ViewModels.Interfaces; // Add this using directive

namespace LinuxInstaller.ViewModels;

public partial class DistroPickerViewModel : ObservableObject, INavigatableViewModel
{
    private readonly DistroService _distroService;
    private readonly InstallationConfigService _installationConfigService; // Injected service

    [ObservableProperty]
    private ObservableCollection<Distro> _distros;

    [ObservableProperty]
    private Distro? _selectedDistro; // Made nullable

    [ObservableProperty]
    private string _searchText;

    private List<Distro> _allDistros;
    private Distro? _previouslySelectedDistro; // Track previously selected distro

    public DistroPickerViewModel(DistroService distroService, InstallationConfigService installationConfigService)
    {
        _distroService = distroService;
        _installationConfigService = installationConfigService;
        _ = LoadDistros();
    }

    private async Task LoadDistros()
    {
        // TODO: Add error handling for when the distro list can't be loaded.
        _allDistros = (await _distroService.GetDistrosAsync()).ToList();
        Distros = new ObservableCollection<Distro>(_allDistros);
    }

    partial void OnSearchTextChanged(string value)
    {
        // This is a basic text search. Could be improved with more advanced filtering.
        if (string.IsNullOrWhiteSpace(value))
        {
            Distros = new ObservableCollection<Distro>(_allDistros);
        }
        else
        {
            Distros = new ObservableCollection<Distro>(_allDistros.Where(d => d.Name.Contains(value, System.StringComparison.OrdinalIgnoreCase)));
        }
    }

    partial void OnSelectedDistroChanged(Distro? value) // Made nullable
    {
        // Deselect previous
        if (_previouslySelectedDistro != null)
        {
            _previouslySelectedDistro.IsSelected = false;
        }

        // Select new
        if (value != null)
        {
            value.IsSelected = true;
            _installationConfigService.SelectedDistro = value;
        } else {
            _installationConfigService.SelectedDistro = null;
        }
        
        _previouslySelectedDistro = value;
    }

    // INavigatableViewModel Implementation
    public bool CanProceed => SelectedDistro != null;
    public bool CanGoBack => true;

    // TODO: A command should be implemented to handle the "Next" button click,
    // which would pass the _selectedDistro to a central state management service or the MainViewModel.
}



