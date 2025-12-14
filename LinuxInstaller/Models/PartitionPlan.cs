using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using System.Linq;

namespace LinuxInstaller.Models;

public class LinuxPartition: Partition
{
    public string MountPoint { get; set; } = string.Empty;
}

public partial class PartitionPlan : ObservableObject
{
    // Represents the disk chosen for installation
    private Disk? _targetDisk;
    public Disk? TargetDisk
    {
        get => _targetDisk;
        set
        {
            SetProperty(ref _targetDisk, value);
            Reset(); // Call Reset when TargetDisk changes
        }
    }

    // History of partition states
    private List<List<Partition>> _partitionHistory = new List<List<Partition>>();
    public List<List<Partition>> PartitionHistory
    {
        get => _partitionHistory;
        set => SetProperty(ref _partitionHistory, value);
    }

    // Constructor
    public PartitionPlan()
    {
        // Partitions and PartitionHistory will be initialized/reset when TargetDisk is set.
    }

    public void Reset()
    {
        PartitionHistory.Clear();
        if (TargetDisk != null)
        {
            // Clone partitions from TargetDisk to ensure independent copies
            PartitionHistory.Add([.. TargetDisk.Partitions.Select(p => p.Clone())]);
        }
    }

    public void AddPartition(Partition newPartition)
    {
        PartitionHistory.Add([.. PartitionHistory.Last(), newPartition]);
    }

    public void EditPartition(Partition oldPartition, Partition updatedPartition)
    {
        var index = PartitionHistory.Last().IndexOf(oldPartition);
        if (index != -1)
        {
            List<Partition> partitions = [.. PartitionHistory.Last()];
            partitions[index] = updatedPartition;
            PartitionHistory.Add(partitions);
        }
    }

    public void DeletePartition(Partition partitionToDelete)
    {
        PartitionHistory.Add([.. PartitionHistory.Last().Where(p => p.Id != partitionToDelete.Id)]);
    }

    public bool IsValid => PartitionHistory.Last().Any(p => p is LinuxPartition l && l.MountPoint == "/");
}