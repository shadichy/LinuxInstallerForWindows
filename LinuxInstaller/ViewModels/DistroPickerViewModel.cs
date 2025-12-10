using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LinuxInstaller.Models;
using LinuxInstaller.Services;
using LinuxInstaller.ViewModels.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls.ApplicationLifetimes;
using LinuxInstaller.Views;

namespace LinuxInstaller.ViewModels;

public partial class DistroPickerViewModel : NavigatableViewModelBase
{
    private readonly DistroService _distroService;
    private readonly InstallationConfigService _installationConfigService;

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
        _distros = new ObservableCollection<Distro>();
        _searchText = string.Empty;
        _allDistros = new List<Distro>();
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
        if (string.IsNullOrWhiteSpace(value))
        {
            Distros = new ObservableCollection<Distro>(_allDistros);
        }
        else
        {
            Distros = new ObservableCollection<Distro>(_allDistros.Where(d => d.DistroName.Contains(value, System.StringComparison.OrdinalIgnoreCase)));
        }
    }


    [RelayCommand]
    private async Task SelectDistro(Distro distro)
    {
        SelectedDistro = distro; // This will trigger OnSelectedDistroChanged if the distro is actually different.
        _installationConfigService.SelectedDistro = distro; // Ensure config is always updated

        var options = new List<DialogOption<PartitionWorkflowType>>
        {
            new() { Label = "Automatic Partitioning", Value = PartitionWorkflowType.Automatic, ButtonStyles = new() { Variant = ButtonVariant.Filled, Size = ButtonSize.Large } },
            new() { Label = "Manual Partitioning", Value = PartitionWorkflowType.Manual, ButtonStyles = new() { Variant = ButtonVariant.Tonal, Size = ButtonSize.Large } }
        };

        var dialog = new MultiOptionDialogView();
        dialog.DataContext = new MultiOptionDialogViewModel<PartitionWorkflowType>("Partitioning Options", "How would you like to manage your disk partitions?", options, dialog);

        if (Avalonia.Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var result = await dialog.ShowDialog<PartitionWorkflowType>(desktop.MainWindow);

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
    private void GoBack()
    {
        Navigation.Previous();
    }
}