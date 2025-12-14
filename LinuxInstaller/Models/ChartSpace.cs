using System;

namespace LinuxInstaller.Models;

public abstract class ChartSpace
{
    public required UInt64 Start { get; set; }
    public required UInt64 Size { get; set; }
    public UInt64 End => Start + Size;
}

public sealed class ChartFreeSpace : ChartSpace
{
}

public sealed class ChartPartition : ChartSpace
{
    public required Partition Partition { get; set; }
}
