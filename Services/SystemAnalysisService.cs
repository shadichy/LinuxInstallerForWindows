namespace LinuxInstaller.Services;

public class SystemAnalysisService
{
    public bool IsRunningAsAdmin()
    {
        // Placeholder: Assume admin rights for dry-run
        return true;
    }

    public void RelaunchAsAdmin()
    {
        // Placeholder: No-op for dry-run
    }

    public int GetBitLockerStatus(string driveLetter = "C:")
    {
        // Placeholder: 0=Unencrypted, 1=Encrypted (Protection On), 2=Encrypted (Protection Off)
        // Assume unencrypted for dry-run
        return 0;
    }

    public string GetBootMode()
    {
        // Placeholder: Assume UEFI for dry-run
        return "UEFI";
    }

    public bool GetSecureBootStatus()
    {
        // Placeholder: Assume Secure Boot is enabled for dry-run
        return true;
    }
}
