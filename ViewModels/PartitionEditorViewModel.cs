using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.IO;
using System.Threading.Tasks;
using System.Diagnostics;
using System;

namespace LinuxInstaller.ViewModels;

public partial class PartitionEditorViewModel : ObservableObject
{
    [ObservableProperty]
    private string _driveLetter = "C";

    [ObservableProperty]
    private int _shrinkSizeInMB = 50000;

    [RelayCommand]
    private async Task Shrink()
    {
        await ShrinkPartition(DriveLetter, ShrinkSizeInMB);
    }

    public async Task<bool> ShrinkPartition(string driveLetter, int sizeInMB)
    {
        string scriptFile = Path.Combine(Path.GetTempPath(), "shrink.txt");
        string script = $"select volume {driveLetter}\nshrink desired={sizeInMB}\n";
        await File.WriteAllTextAsync(scriptFile, script);

        var startInfo = new ProcessStartInfo("diskpart.exe", $"/s \"{scriptFile}\"")
        {
            UseShellExecute = false,
            CreateNoWindow = true,
            RedirectStandardError = true
        };

        using (var process = Process.Start(startInfo))
        {
            string error = await process.StandardError.ReadToEndAsync();
            await process.WaitForExitAsync();
            File.Delete(scriptFile);
            if (process.ExitCode != 0) throw new Exception($"Diskpart failed: {error}");
            return true;
        }
    }
}

