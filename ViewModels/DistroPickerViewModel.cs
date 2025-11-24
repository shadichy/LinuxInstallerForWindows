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
        _distroService = new DistroService();
        _ = LoadDistros();
    }

    private async Task LoadDistros()
    {
        _allDistros = (await _distroService.GetDistrosAsync()).ToList();
        Distros = new ObservableCollection<Distro>(_allDistros);
    }

    partial void OnSearchTextChanged(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            Distros = new ObservableCollection<Distro>(_allDistros);
        }
        else
        {
            Distros = new ObservableCollection<Distro>(_allDistros.Where(d => d.Name.Contains(value, System.StringComparison.OrdinalIgnoreCase)));
        }
    }
}
