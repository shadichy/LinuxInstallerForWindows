namespace LinuxInstaller.Models;

public class Partition
{
    public string Id { get; set; } = string.Empty; // 128-byte partiton uuid
    public string Name { get; set; } = string.Empty;
    public ulong Size { get; set; } // in bytes
    public ulong StartOffset { get; set; } // in bytes
    public string FileSystem { get; set; } = string.Empty;
    public bool IsBoot { get; set; }
    public bool IsSystem { get; set; }
}
