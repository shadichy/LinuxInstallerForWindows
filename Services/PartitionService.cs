using LinuxInstaller.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace LinuxInstaller.Services;

public class PartitionService
{
    public Task<bool> ShrinkPartition(string driveLetter, int sizeInMB)
    {
        // Placeholder: Assume shrink is successful
        return Task.FromResult(true);
    }

    public ObservableCollection<Disk> GetAvailableDisks()
    {
        // Placeholder: Return example data
        return new ObservableCollection<Disk>
        {
            new Disk { Id = "disk0", Name = "SAMSUNG 970 EVO Plus 1TB", Size = 1_000_000_000_000, IsBootable = true },
            new Disk { Id = "disk1", Name = "Crucial MX500 2TB", Size = 2_000_000_000_000, IsBootable = false }
        };
    }

    public ObservableCollection<Partition> GetPartitions(string diskId)
    {
        // Placeholder: Return example data
        if (diskId == "disk0")
        {
            return new ObservableCollection<Partition>
            {
                new Partition { Id = "p1", Name = "ESP", Size = 500 * 1024 * 1024, FileSystem = "FAT32", IsBoot = true, IsSystem = true },
                new Partition { Id = "p2", Name = "Windows (C:)", Size = 800_000_000_000, FileSystem = "NTFS", IsBoot = false, IsSystem = false },
                new Partition { Id = "p3", Name = "Recovery", Size = 1_000_000_000, FileSystem = "NTFS", IsBoot = false, IsSystem = true }
            };
        }
        else
        {
            return new ObservableCollection<Partition>();
        }
    }
}
