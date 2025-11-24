namespace LinuxInstaller.Services;

public class BootManagerService
{
    public string MountEsp()
    {
        // Placeholder: Assume ESP is mounted to S: for dry-run
        return "S:";
    }

    public void UnmountEsp()
    {
        // Placeholder: No-op for dry-run
    }

    public void CreateBcdEntry(string espPath, string efiRelativePath)
    {
        // Placeholder: No-op for dry-run
    }

    public void RemoveBcdEntry()
    {
        // Placeholder: No-op for dry-run
    }
}
