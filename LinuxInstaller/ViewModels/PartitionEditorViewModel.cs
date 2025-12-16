using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LinuxInstaller.Models;
using LinuxInstaller.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using LinuxInstaller.ViewModels.Interfaces;
using System.Linq;
using Avalonia.Controls.ApplicationLifetimes;
using LinuxInstaller.Views;

namespace LinuxInstaller.ViewModels;

public partial class PartitionEditorViewModel : NavigatableViewModelBase
{
    private readonly PartitionService _partitionService;
    private readonly InstallationConfigService _installationConfigService;

    [ObservableProperty]
    private ObservableCollection<Disk> _disks;

    [ObservableProperty]
    private ObservableCollection<ChartSpace> _diskLayoutChart;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CanAddPartition))]
    [NotifyPropertyChangedFor(nameof(CanEditPartition))]
    [NotifyPropertyChangedFor(nameof(CanDeletePartition))]
    private ChartSpace? _selectedChartSpace;

    [ObservableProperty]
    private PartitionPlan _plan = new();

    // SelectedDisk is now managed by InstallationConfigService.PartitionPlan.TargetDisk
    public Disk? SelectedDisk
    {
        get => Plan.TargetDisk;
        set
        {
            var oldDisk = Plan.TargetDisk;

            if (oldDisk == null)
            {
                Plan.TargetDisk = value;
                OnPropertyChanged(); // Notify UI that SelectedDisk has changed
                RefreshSelectedPart(value);
            }

            if (oldDisk == value) return;

            if (_installationConfigService.PartitionPlan.PartitionHistory.Count == 1)
            {
                Plan.TargetDisk = value;
                OnPropertyChanged(); // Notify UI that SelectedDisk has changed
                RefreshSelectedPart(value);
                return;
            }

            var dialog = new ConfirmationDialogView();
            dialog.DataContext = new ConfirmationDialogViewModel("You have unsaved changes. Are you sure you want to discard them?", dialog);

            if (Avalonia.Application.Current?.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop) return;

            Avalonia.Controls.Window? mainWindow = desktop.MainWindow;
            if (mainWindow == null) return;

            dialog.ShowDialog<bool>(mainWindow).ContinueWith(task =>
            {
                if (task.Result)
                {
                    Plan.TargetDisk = value;
                    OnPropertyChanged(); // Notify UI that SelectedDisk has changed
                    RefreshSelectedPart(value);
                }
                else
                {
                    // User cancelled, so revert the selection
                    Plan.TargetDisk = oldDisk;
                    OnPropertyChanged();
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }
    }

    public PartitionEditorViewModel(NavigationService navigationService, PartitionService partitionService, InstallationConfigService installationConfigService) : base(navigationService)
    {
        _partitionService = partitionService;
        _installationConfigService = installationConfigService;
        _disks = [];
        _diskLayoutChart = [];

        // Load initial data
        RefreshDisks();
    }

    private void RefreshDisks()
    {
        var availableDisks = _partitionService.GetAvailableDisks();
        Disks = new ObservableCollection<Disk>(availableDisks);

        if (Disks.Count > 0 && SelectedDisk == null)
        {
            SelectedDisk = Disks.First(); // Automatically select the first disk
        }

        if (SelectedDisk != null)
        {
            RefreshSelectedPart(SelectedDisk);
        }

        // Re-evaluate CanProceed when disks/partitions are refreshed
        OnPropertyChanged(nameof(CanProceed));
    }

    private void RefreshSelectedPart(Disk? disk)
    {
        if (disk != null)
        {
            UpdateChart();
        }
        else
        {
            DiskLayoutChart = [];
        }
    }

    private void UpdateChart()
    {
        if (SelectedDisk == null) return;

        var newLayout = new ObservableCollection<ChartSpace>();
        var sortedPartitions = Plan.PartitionHistory.Last().OrderBy(p => p.StartOffset).ToList();
        ulong lastPosition = 0;

        foreach (var partition in sortedPartitions)
        {
            // Gap before this partition
            ulong gap = partition.StartOffset - lastPosition;
            if (gap > 1024 * 1024) // Leading space reserved for bootloader
            {
                newLayout.Add(new ChartFreeSpace { Start = lastPosition, Size = gap });
            }

            // This partition
            var chartPartition = new ChartPartition { Start = partition.StartOffset, Size = partition.Size, Partition = partition };
            newLayout.Add(chartPartition);

            lastPosition = chartPartition.End;
        }

        // Final gap at the end of the disk
        ulong finalGap = SelectedDisk.Size - lastPosition;
        if (finalGap > 0)
        {
            newLayout.Add(new ChartFreeSpace { Start = lastPosition, Size = finalGap });
        }

        DiskLayoutChart = newLayout;
    }

    // INavigatableViewModel Implementation
    public override bool CanProceed => Plan.IsValid;
    public override bool CanGoBack => true;

    // TODO: Add commands for creating, deleting, and editing partitions.
    // These commands would manipulate a "PartitionPlan" object that gets passed to the summary view
    // and ultimately to the ConfigGeneratorService.

    [RelayCommand]
    public void Back()
    {
        Navigation.Previous();
    }

    [RelayCommand]
    public void Next()
    {
        _installationConfigService.PartitionPlan = Plan;
        Navigation.Next();
    }

    public bool CanAddPartition => SelectedChartSpace is ChartFreeSpace;
    public bool CanEditPartition => SelectedChartSpace is ChartPartition p && !p.Partition.IsSystem;
    public bool CanDeletePartition => SelectedChartSpace is ChartPartition p && !p.Partition.IsSystem;


    [RelayCommand]
    public async Task AddPartition()
    {
        var dialog = new PartitionDialogView();
        dialog.DataContext = new PartitionDialogViewModel(dialog, SelectedChartSpace!, Plan.PartitionHistory.Last().Count, Plan.IsValid);

        if (Avalonia.Application.Current?.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop) return;

        var newPartition = await dialog.ShowDialog<PlannedPartition>(desktop.MainWindow!);
        if (newPartition == null) return;

        Plan.AddPartition(newPartition);
        UpdateChart(); // Refresh the chart
        OnPropertyChanged(nameof(CanProceed));
    }

    [RelayCommand]
    public async Task EditPartition()
    {
        var partition = ((ChartPartition)SelectedChartSpace!).Partition;
        var dialog = new PartitionDialogView();
        dialog.DataContext = new PartitionDialogViewModel(dialog, SelectedChartSpace!);

        if (Avalonia.Application.Current?.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop) return;

        var updatedPartition = await dialog.ShowDialog<PlannedPartition>(desktop.MainWindow!);
        if (updatedPartition == null) return;

        // Preserve properties that should not be changed by the dialog
        updatedPartition.Id = partition.Id;
        updatedPartition.StartOffset = partition.StartOffset;

        Plan.EditPartition(partition, updatedPartition);
        UpdateChart(); // Refresh the chart
        OnPropertyChanged(nameof(CanProceed));
    }

    [RelayCommand]
    public void DeletePartition()
    {
        var partition = ((ChartPartition)SelectedChartSpace!).Partition;
        Plan.DeletePartition(partition);
        UpdateChart(); // Refresh the chart
        OnPropertyChanged(nameof(CanProceed));
    }
}
