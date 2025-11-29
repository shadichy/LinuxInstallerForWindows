namespace LinuxInstaller.Models;

public class Partition
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public long Size { get; set; } // in bytes
    public string FileSystem { get; set; } = string.Empty;
    public bool IsBoot { get; set; }
    public bool IsSystem { get; set; }
}
