namespace LinuxInstaller.Models;

public class Disk
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public long Size { get; set; } // in bytes
    public bool IsBootable { get; set; }
}
