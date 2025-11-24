namespace LinuxInstaller.Models;

public class Partition
{
    public string Id { get; set; }
    public string Name { get; set; }
    public long Size { get; set; } // in bytes
    public string FileSystem { get; set; }
    public bool IsBoot { get; set; }
    public bool IsSystem { get; set; }
}
