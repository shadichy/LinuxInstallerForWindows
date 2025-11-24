using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LinuxInstaller.Models;
using LinuxInstaller.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using LinuxInstaller.ViewModels.Interfaces;
using System.Linq;

namespace LinuxInstaller.ViewModels;

public partial class PartitionEditorViewModel : ObservableObject, INavigatableViewModel
{
    private readonly PartitionService _partitionService;
    private readonly InstallationConfigService _installationConfigService;

    [ObservableProperty]
    private ObservableCollection<Disk> _disks;

    [ObservableProperty]
    private ObservableCollection<Partition> _partitions;

    // SelectedDisk is now managed by InstallationConfigService.PartitionPlan.TargetDisk
    public Disk? SelectedDisk
    {
        get => _installationConfigService.PartitionPlan.TargetDisk;
        set
        {
            if (_installationConfigService.PartitionPlan.TargetDisk != value)
            {
                _installationConfigService.PartitionPlan.TargetDisk = value;
                OnPropertyChanged(); // Notify UI that SelectedDisk has changed
                _ = RefreshPartitionsForSelectedDiskAsync(value);
            }
        }
    }

    // ShrinkSizeInMB is now managed by InstallationConfigService.PartitionPlan.ShrinkSizeInMB
    public int ShrinkSizeInMB
    {
        get => _installationConfigService.PartitionPlan.ShrinkSizeInMB;
        set
        {
            if (_installationConfigService.PartitionPlan.ShrinkSizeInMB != value)
            {
                _installationConfigService.PartitionPlan.ShrinkSizeInMB = value;
                OnPropertyChanged(); // Notify UI that ShrinkSizeInMB has changed
            }
        }
    }


    public PartitionEditorViewModel(PartitionService partitionService, InstallationConfigService installationConfigService)
    {
        _partitionService = partitionService;
        _installationConfigService = installationConfigService;
        
        // Initialize ShrinkSizeInMB if it's default
        if (ShrinkSizeInMB == 0) // Default value from PartitionPlan's constructor
        {
            ShrinkSizeInMB = 50000; // Set a default initial value
        }

        // Load initial data
        _ = RefreshDisksAndPartitionsAsync();
    }

    private async Task RefreshDisksAndPartitionsAsync()
    {
        var availableDisks = _partitionService.GetAvailableDisks();
        Disks = new ObservableCollection<Disk>(availableDisks);

        if (Disks.Count > 0 && SelectedDisk == null)
        {
            SelectedDisk = Disks.First(); // Automatically select the first disk
        }

        if (SelectedDisk != null)
        {
            await RefreshPartitionsForSelectedDiskAsync(SelectedDisk);
        }
        
        // Re-evaluate CanProceed when disks/partitions are refreshed
        OnPropertyChanged(nameof(CanProceed));
    }

    private async Task RefreshPartitionsForSelectedDiskAsync(Disk? disk)
    {
        if (disk != null)
        {
            // Assuming GetPartitions can be made async or wraps an async operation
            // For now, if GetPartitions returns a concrete list, we will just assign it.
            // If PartitionService itself is updated to return Task<IEnumerable<Partition>>,
            // this part would need adjustment.
            Partitions = new ObservableCollection<Partition>(await _partitionService.GetPartitionsAsync(disk.Id));
        } else {
            Partitions = new ObservableCollection<Partition>();
        }
    }

    [RelayCommand]
    private async Task Shrink()
    {
        if (SelectedDisk != null)
        {
            await _partitionService.ShrinkPartition(SelectedDisk.Name, ShrinkSizeInMB);
            await RefreshDisksAndPartitionsAsync(); // Refresh the partition list after shrinking
        }
    }

    // INavigatableViewModel Implementation
    public bool CanProceed => _installationConfigService.PartitionPlan.IsValid;
    public bool CanGoBack => true;

    // TODO: Add commands for creating, deleting, and editing partitions.
    // These commands would manipulate a "PartitionPlan" object that gets passed to the summary view
    // and ultimately to the ConfigGeneratorService.
}
