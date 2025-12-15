using LinuxInstaller.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LinuxInstaller.Services;

// TODO: Replace all placeholder logic in this service with real system calls (e.g., Diskpart, WMI).
public class PartitionService
{
    public Task<bool> ShrinkPartition(string driveLetter, int sizeInMB)
    {
        // TODO: Use a real diskpart wrapper (like DiskpartService) to shrink the partition.
        // This operation would typically be much longer and involve actual system calls.
        return Task.FromResult(true);
    }

    public List<Disk> GetAvailableDisks()
    {
        // TODO: Implement real enumeration of disks on the system, for example by parsing `diskpart list disk`
        // or by using WMI (`Win32_DiskDrive`).
        return
        [
            new() {
                Id = "disk0",
                Name = "SAMSUNG 970 EVO Plus 1TB",
                Size = 1_000_000_000_000UL,
                IsBootable = true,
                Partitions =
                [
                    new() { Id = "p1", Name = "ESP", Size = 500UL * 1024 * 1024, StartOffset = 1024UL * 1024, FileSystem = FileSystem.FAT32, IsBoot = true, IsSystem = true },
                    new() { Id = "p2", Name = "Windows (C:)", Size = 800_000_000_000UL, StartOffset = 525_336_576UL, FileSystem = FileSystem.NTFS, IsBoot = false, IsSystem = true },
                    new() { Id = "p3", Name = "Recovery", Size = 1_000_000_000UL, StartOffset = 800_525_336_576UL, FileSystem = FileSystem.NTFS, IsBoot = false, IsSystem = true }
                ]
            },
            new() {
                Id = "disk1",
                Name = "Crucial MX500 2TB",
                Size = 2_000_000_000_000UL,
                IsBootable = false,
                Partitions = [] // No partitions for this disk
            }
        ];
    }

    public List<Partition> GetPartitions(string diskId)
    {
        // TODO: Implement real enumeration of partitions for a given disk, for example by parsing `diskpart list partition`
        // or by using WMI (`Win32_DiskPartition`).
        if (diskId == "disk0")
        {
            var partitions = new List<Partition>
            {
                new() { Id = "p1", Name = "ESP", Size = 500UL * 1024 * 1024, StartOffset = 1024UL * 1024, FileSystem = FileSystem.FAT32, IsBoot = true, IsSystem = true },
                new() { Id = "p2", Name = "Windows (C:)", Size = 800_000_000_000UL, StartOffset = 525_336_576UL, FileSystem = FileSystem.NTFS, IsBoot = false, IsSystem = true },
                new() { Id = "p3", Name = "Recovery", Size = 1_000_000_000UL, StartOffset = 800_525_336_576UL, FileSystem = FileSystem.NTFS, IsBoot = false, IsSystem = true }
            };
            return partitions;
        }
        else
        {
            return new List<Partition>();
        }
    }
}
