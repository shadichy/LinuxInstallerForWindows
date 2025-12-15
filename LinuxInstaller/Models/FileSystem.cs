namespace LinuxInstaller.Models;

public enum FileSystem
{
    Unknown,
    // Windows file systems
    NTFS,
    FAT32,
    EXFAT,
    // Targeted Linux file systems
    LINUX, // Default EXT4
}

public sealed class FS
{
    public static string ToString(FileSystem fs) => fs switch
    {
        FileSystem.NTFS => "NTFS",
        FileSystem.FAT32 => "FAT32",
        FileSystem.EXFAT => "exFAT",
        FileSystem.LINUX => "EXT4",
        _ => "Unknown"
    };
    public static FileSystem ToFileSystem(string fs) => fs.ToUpper() switch
    {
        "NTFS" => FileSystem.NTFS,
        "FAT32" => FileSystem.FAT32,
        "EXFAT" => FileSystem.EXFAT,
        "EXT4" or "LINUX" => FileSystem.LINUX,
        _ => FileSystem.Unknown
    };
}