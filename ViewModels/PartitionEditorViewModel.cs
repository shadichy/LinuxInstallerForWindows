using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LinuxInstaller.Models;
using LinuxInstaller.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace LinuxInstaller.ViewModels;

public partial class PartitionEditorViewModel : ObservableObject
{
    private readonly PartitionService _partitionService;

    [ObservableProperty]
    private ObservableCollection<Disk> _disks;

    [ObservableProperty]
    private ObservableCollection<Partition> _partitions;

    [ObservableProperty]
    private Disk _selectedDisk;

    [ObservableProperty]
    private int _shrinkSizeInMB = 50000;

    public PartitionEditorViewModel()
    {
        // TODO: In a real app, services should be injected via Dependency Injection
        // instead of being instantiated directly.
        _partitionService = new PartitionService();
        Disks = _partitionService.GetAvailableDisks();
        if (Disks.Count > 0)
        {
            SelectedDisk = Disks[0];
        }
    }

    // TODO: This should be async Task and await the service call.
    partial void OnSelectedDiskChanged(Disk value)
    {
        if (value != null)
        {
            // This is using placeholder data. A real implementation would await GetPartitions.
            Partitions = _partitionService.GetPartitions(value.Id);
        }
    }

    [RelayCommand]
    private async Task Shrink()
    {
        if (SelectedDisk != null)
        {
            await _partitionService.ShrinkPartition(SelectedDisk.Name, ShrinkSizeInMB);
            // TODO: Refresh the partition list from the service after shrinking.
        }
    }

    // TODO: Add commands for creating, deleting, and editing partitions.
    // These commands would manipulate a "PartitionPlan" object that gets passed to the summary view
    // and ultimately to the ConfigGeneratorService.
}

