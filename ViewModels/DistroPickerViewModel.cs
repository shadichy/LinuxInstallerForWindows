using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LinuxInstaller.Models;
using LinuxInstaller.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace LinuxInstaller.ViewModels;

public partial class DistroPickerViewModel : ObservableObject
{
    private readonly DistroService _distroService;

    [ObservableProperty]
    private ObservableCollection<Distro> _distros;

    [ObservableProperty]
    private Distro _selectedDistro;

    [ObservableProperty]
    private string _searchText;

    private List<Distro> _allDistros;

    public DistroPickerViewModel()
    {
        // TODO: In a real app, services should be injected via Dependency Injection.
        _distroService = new DistroService();
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

    // TODO: A command should be implemented to handle the "Next" button click,
    // which would pass the _selectedDistro to a central state management service or the MainViewModel.
}
