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
    public required PlannedPartition Partition { get; set; }

    public static ChartPartition FromPartition(PlannedPartition partition)
    {
        return new ChartPartition
        {
            Start = partition.StartOffset,
            Size = partition.Size,
            Partition = partition
        };
    }
}
