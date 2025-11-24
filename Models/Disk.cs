namespace LinuxInstaller.Models;

public class Disk
{
    public string Id { get; set; }
    public string Name { get; set; }
    public long Size { get; set; } // in bytes
    public bool IsBootable { get; set; }
}
