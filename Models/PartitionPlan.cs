namespace LinuxInstaller.Models;

public class PartitionPlan
{
    public string MountPoint { get; set; }
    public string FileSystem { get; set; }
    public long SizeInMb { get; set; }
}
