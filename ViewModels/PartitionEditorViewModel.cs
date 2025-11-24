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
        _partitionService = new PartitionService();
        Disks = _partitionService.GetAvailableDisks();
        if (Disks.Count > 0)
        {
            SelectedDisk = Disks[0];
            OnSelectedDiskChanged(SelectedDisk);
        }
    }

    partial void OnSelectedDiskChanged(Disk value)
    {
        if (value != null)
        {
            Partitions = _partitionService.GetPartitions(value.Id);
        }
    }

    [RelayCommand]
    private async Task Shrink()
    {
        if (SelectedDisk != null)
        {
            await _partitionService.ShrinkPartition(SelectedDisk.Name, ShrinkSizeInMB);
            // In a real app, you would refresh the partition list here
        }
    }
}

