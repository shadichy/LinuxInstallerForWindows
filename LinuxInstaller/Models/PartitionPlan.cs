using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using System.Linq;

namespace LinuxInstaller.Models;

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
    private List<List<PlannedPartition>> _partitionHistory = [];
    public List<List<PlannedPartition>> PartitionHistory
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
            PartitionHistory.Add([.. TargetDisk.Partitions.Select(p => PlannedPartition.FromPartition(p))]);
        }
    }

    public void AddPartition(PlannedPartition newPartition)
    {
        PartitionHistory.Add([.. PartitionHistory.Last(), newPartition]);
    }

    public void EditPartition(PlannedPartition oldPartition, PlannedPartition updatedPartition)
    {
        var index = PartitionHistory.Last().IndexOf(oldPartition);
        if (index != -1)
        {
            List<PlannedPartition> partitions = [.. PartitionHistory.Last()];
            partitions[index] = updatedPartition;
            PartitionHistory.Add(partitions);
        }
    }

    public void DeletePartition(PlannedPartition partitionToDelete)
    {
        PartitionHistory.Add([.. PartitionHistory.Last().Where(p => p.Id != partitionToDelete.Id)]);
    }

    public bool IsValid => PartitionHistory.Last().Any(p => p is PlannedPartition l && l.MountPoint == "/");
}