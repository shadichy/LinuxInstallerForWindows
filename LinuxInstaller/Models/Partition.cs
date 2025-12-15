using System;
using System.Buffers.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LinuxInstaller.Models;

public class Partition
{
    public required string Id { get; set; } = string.Empty; // 128-byte partiton uuid
    public string Name { get; set; } = string.Empty;
    public required ulong Size { get; set; } // in bytes
    public required ulong StartOffset { get; set; } // in bytes
    public FileSystem FileSystem { get; set; }
    public bool IsBoot { get; set; }
    public required bool IsSystem { get; set; }

    public ulong EndOffset => StartOffset + Size;

    virtual public Partition Clone()
    {
        return new()
        {
            Id = Id,
            Name = Name,
            Size = Size,
            StartOffset = StartOffset,
            FileSystem = FileSystem,
            IsBoot = IsBoot,
            IsSystem = IsSystem
        };
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Partition other) return false;
        return GetHashCode() == other.GetHashCode();
    }

    public override int GetHashCode()
    {
        // conbination of id, size, and start offset should be unique enough
        return HashCode.Combine(Id, Size, StartOffset);
    }
}

public class PlannedPartition : Partition
{
    public string MountPoint { get; set; } = string.Empty;

    public static PlannedPartition FromPartition(Partition partition, string mountPoint = "")
    {
        return new PlannedPartition
        {
            Id = partition.Id,
            Name = partition.Name,
            Size = partition.Size,
            StartOffset = partition.StartOffset,
            FileSystem = partition.FileSystem,
            IsBoot = partition.IsBoot,
            IsSystem = partition.IsSystem,
            MountPoint = mountPoint
        };
    }

    public override PlannedPartition Clone()
    {
        return new PlannedPartition
        {
            Id = Id,
            Name = Name,
            Size = Size,
            StartOffset = StartOffset,
            FileSystem = FileSystem,
            IsBoot = IsBoot,
            IsSystem = IsSystem,
            MountPoint = MountPoint
        };
    }
}