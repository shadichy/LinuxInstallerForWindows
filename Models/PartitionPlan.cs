using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace LinuxInstaller.Models
{
    public partial class PartitionPlan : ObservableObject
    {
        // Represents the disk chosen for installation
        private Disk? _targetDisk;
        public Disk? TargetDisk
        {
            get => _targetDisk;
            set => SetProperty(ref _targetDisk, value);
        }

        // Represents the partition to be shrunk, if any
        private Partition? _partitionToShrink;
        public Partition? PartitionToShrink
        {
            get => _partitionToShrink;
            set => SetProperty(ref _partitionToShrink, value);
        }

        // The size to shrink the partition by (in MB)
        private int _shrinkSizeInMB;
        public int ShrinkSizeInMB
        {
            get => _shrinkSizeInMB;
            set => SetProperty(ref _shrinkSizeInMB, value);
        }

        // Collection of planned partitions for Linux (e.g., /, /home, swap)
        private ObservableCollection<Partition> _linuxPartitions = new ObservableCollection<Partition>();
        public ObservableCollection<Partition> LinuxPartitions
        {
            get => _linuxPartitions;
            set => SetProperty(ref _linuxPartitions, value);
        }

        // Constructor
        public PartitionPlan()
        {
            // Initialize with default values or empty collections
        }

        // TODO: Add methods for validating the partition plan
        public bool IsValid => TargetDisk != null && LinuxPartitions.Count > 0 && ShrinkSizeInMB > 0;
    }
}