using System.Collections.Generic;

namespace LinuxInstaller.Models;

public class Disk
{
    public required string Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public required ulong Size { get; set; } // in bytes
    public bool IsBootable { get; set; }
    public required List<Partition> Partitions { get; set; }
}
