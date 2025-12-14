namespace LinuxInstaller.Models;

public abstract class ChartSpace
{
    public required ulong Start { get; set; }
    public required ulong Size { get; set; }
    public ulong End => Start + Size;
}

public sealed class ChartFreeSpace : ChartSpace
{
}

public sealed class ChartPartition : ChartSpace
{
    public required Partition Partition { get; set; }
}
