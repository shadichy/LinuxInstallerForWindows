using System.IO;
using System.Reflection;

namespace LinuxInstaller.Services;

public class AssetManager
{
    public void CopyPrebuiltConfigs(string targetPath)
    {
        Directory.CreateDirectory(targetPath);

        CopyResource("LinuxInstaller.prebuilt.stage1.cfg", Path.Combine(targetPath, "stage1.cfg"));
        CopyResource("LinuxInstaller.prebuilt.stage2.cfg", Path.Combine(targetPath, "stage2.cfg"));
        CopyResource("LinuxInstaller.prebuilt.install.conf", Path.Combine(targetPath, "install.conf"));
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
