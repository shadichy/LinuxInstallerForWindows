using Avalonia.Controls.ApplicationLifetimes;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LinuxInstaller.Models;
using LinuxInstaller.Services;
using LinuxInstaller.ViewModels.Interfaces;
using LinuxInstaller.Views;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace LinuxInstaller.ViewModels;

public partial class DistroPickerViewModel : NavigatableViewModelBase
{
    private readonly DistroService _distroService;
    private readonly InstallationConfigService _installationConfigService;

    public string Title => "Select a Distribution";
    public string Subtitle => "Choose a Linux distribution to install on your machine.";

    [ObservableProperty]
    private ObservableCollection<Distro> _distros;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CanProceed))]
    private Distro? _selectedDistro;

    [ObservableProperty]
    private string _searchText;

    private List<Distro> _allDistros;
    private Distro? _previouslySelectedDistro;

    public DistroPickerViewModel(NavigationService navigationService, DistroService distroService, InstallationConfigService installationConfigService) : base(navigationService)
    {
        _distroService = distroService;
        _installationConfigService = installationConfigService;
        _distros = [];
        _searchText = string.Empty;
        _allDistros = [];
        _ = LoadDistros();
    }

    private async Task LoadDistros()
    {
        // TODO: Add error handling for when the distro list can't be loaded.
        _allDistros = (await _distroService.GetDistros()).ToList();
        Distros = new ObservableCollection<Distro>(_allDistros);
    }

    partial void OnSearchTextChanged(string value)
    {
        // This is a basic text search. Could be improved with more advanced filtering.
        IEnumerable<Distro> result = _allDistros;
        if (!string.IsNullOrWhiteSpace(value)) result = _allDistros.Where(d => d.DistroName.Contains(value, System.StringComparison.OrdinalIgnoreCase));
        Distros = new ObservableCollection<Distro>(result);
    }


    [RelayCommand]
    private async Task SelectDistro(Distro distro)
    {
        SelectedDistro = distro; // This will trigger OnSelectedDistroChanged if the distro is actually different.
        _installationConfigService.SelectedDistro = distro; // Ensure config is always updated

        var options = new List<DialogOption<PartitionWorkflowType>>
        {
            new() { Label = "Manual Partitioning", Value = PartitionWorkflowType.Manual, ButtonStyles = new() { Variant = ButtonVariant.Tonal, Size = ButtonSize.Large } },
            new() { Label = "Automatic Partitioning", Value = PartitionWorkflowType.Automatic, ButtonStyles = new() { Variant = ButtonVariant.Filled, Size = ButtonSize.Large } },
        };

        var dialog = new MultiOptionDialogView();
        dialog.DataContext = new MultiOptionDialogViewModel<PartitionWorkflowType>("Partitioning Options", "How would you like to manage your disk partitions?", options, dialog);

        if (Avalonia.Application.Current?.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop) return;
        var result = await dialog.ShowDialog<PartitionWorkflowType>(desktop.MainWindow!);

        _installationConfigService.SelectedPartitionWorkflow = result;

        if (result == PartitionWorkflowType.Automatic)
        {
            Navigation.Next(2);
        }
        else if (result == PartitionWorkflowType.Manual)
        {
            Navigation.Next();
        }
    }

    partial void OnSelectedDistroChanged(Distro? value)
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
        }
        else
        {
            _installationConfigService.SelectedDistro = null;
        }

        _previouslySelectedDistro = value;
    }


    // INavigatableViewModel Implementation
    public override bool CanProceed => SelectedDistro != null;
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