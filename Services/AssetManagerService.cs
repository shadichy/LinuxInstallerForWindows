using System.IO;
using System.Reflection;

namespace LinuxInstaller.Services;

public class AssetManagerService
{
    public void CopyPrebuiltConfigs(string targetPath)
    {
        Directory.CreateDirectory(targetPath);

        CopyResource("LinuxInstaller.prebuilt.stage1.cfg", Path.Combine(targetPath, "stage1.cfg"));
        CopyResource("LinuxInstaller.prebuilt.stage2.cfg", Path.Combine(targetPath, "stage2.cfg"));
        CopyResource("LinuxInstaller.prebuilt.install.conf", Path.Combine(targetPath, "install.conf"));
    }

    public void CopyBundledBootloader(string espPath)
    {
        string targetDir = Path.Combine(espPath, "EFI", "MyCustomInstaller");
        Directory.CreateDirectory(targetDir);

        // Placeholder resource names. These need to be added as embedded resources in the .csproj
        // For dry-run, the CopyResource method will gracefully handle them not being found.
        CopyResource("LinuxInstaller.Resources.shimx64.efi", Path.Combine(targetDir, "shimx64.efi"));
        CopyResource("LinuxInstaller.Resources.grubx64.efi", Path.Combine(targetDir, "grubx64.efi"));
        CopyResource("LinuxInstaller.Resources.mmx64.efi", Path.Combine(targetDir, "mmx64.efi"));
    }

    private void CopyResource(string resourceName, string destinationPath)
    {
        using (var resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
        {
            if (resourceStream == null)
            {
                // Handle resource not found
                return;
            }

            using (var fileStream = new FileStream(destinationPath, FileMode.Create))
            {
                resourceStream.CopyTo(fileStream);
            }
        }
    }
}
