using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using LinuxInstaller.Models;

namespace LinuxInstaller.Services;

public class DiskpartService
{
    public async Task<bool> ShrinkPartition(string driveLetter, int sizeInMB)
    {
        string script = $"select volume {driveLetter}\nshrink desired={sizeInMB}";
        var result = await ExecuteScriptAsync(script);
        // TODO: A real implementation should parse stdout to confirm success, not just rely on exit code.
        return result.exitCode == 0 && string.IsNullOrEmpty(result.stdErr);
    }
    
    public async Task<List<Disk>> ListDisksAsync()
    {
        var (exitCode, stdout, stdErr) = await ExecuteScriptAsync("list disk");
        if (exitCode != 0)
        {
            throw new System.Exception($"Diskpart failed to list disks: {stdErr}");
        }
        
        // TODO: Implement real parsing of the 'stdout' string from diskpart to build the List<Disk>.
        return new List<Disk>
        {
            new Disk { Id = "disk0", Name = "Disk 0", Size = 1_000_204_886_016, IsBootable = true },
            new Disk { Id = "disk1", Name = "Disk 1", Size = 2_000_398_934_016, IsBootable = false }
        };
    }
    
    public async Task<List<string>> ListVolumesAsync()
    {
        var (exitCode, stdout, stdErr) = await ExecuteScriptAsync("list volume");
        if (exitCode != 0)
        {
            throw new System.Exception($"Diskpart failed to list volumes: {stdErr}");
        }
        
        // TODO: Implement real parsing of the 'stdout' string from diskpart to build a structured list of volumes.
        return new List<string>
        {
            "Volume 0, C, NTFS, Partition, 931 GB",
            "Volume 1, D, NTFS, Partition, 1863 GB",
            "Volume 2, , FAT32, Partition, 500 MB, System"
        };
    }

    public async Task<(int exitCode, string stdout, string stdErr)> ExecuteScriptAsync(string scriptContent)
    {
        // This is a functional wrapper, but running diskpart has security implications
        // and requires administrator privileges.
        string scriptFile = Path.Combine(Path.GetTempPath(), $"diskpart_{Path.GetRandomFileName()}.txt");
        await File.WriteAllTextAsync(scriptFile, scriptContent);

        var startInfo = new ProcessStartInfo("diskpart.exe", $"/s \"{scriptFile}\"")
        {
            UseShellExecute = false,
            CreateNoWindow = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            StandardOutputEncoding = Encoding.UTF8
        };

        using (var process = new Process { StartInfo = startInfo })
        {
            var outputBuilder = new StringBuilder();
            var errorBuilder = new StringBuilder();

            process.OutputDataReceived += (sender, args) => outputBuilder.AppendLine(args.Data);
            process.ErrorDataReceived += (sender, args) => errorBuilder.AppendLine(args.Data);
            
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            
            await process.WaitForExitAsync();
            
            File.Delete(scriptFile);

            return (process.ExitCode, outputBuilder.ToString(), errorBuilder.ToString());
        }
    }
}
