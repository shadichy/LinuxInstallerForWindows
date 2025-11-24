using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Security.Principal;
using System.Diagnostics;
using System;
using System.Management;

namespace LinuxInstaller.ViewModels;

public partial class PreFlightCheckViewModel : ObservableObject
{
    [ObservableProperty]
    private bool _isAdmin;

    [ObservableProperty]
    private bool _isUefi;

    [ObservableProperty]
    private bool _isSecureBootEnabled;

    [ObservableProperty]
    private bool _isBitLockerEnabled;

    public PreFlightCheckViewModel()
    {
        IsAdmin = IsRunningAsAdmin();
        IsBitLockerEnabled = GetBitLockerStatus() == 1;
    }

    [RelayCommand]
    private void RelaunchAsAdmin()
    {
        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = Process.GetCurrentProcess().MainModule.FileName,
            UseShellExecute = true,
            Verb = "runas"
        };
        Process.Start(startInfo);
        Environment.Exit(0);
    }

    private bool IsRunningAsAdmin()
    {
        var identity = WindowsIdentity.GetCurrent();
        var principal = new WindowsPrincipal(identity);
        return principal.IsInRole(WindowsBuiltInRole.Administrator);
    }

    private int GetBitLockerStatus(string driveLetter = "C:")
    {
        try
        {
            string wmiNamespace = "\\.\\root\\CIMV2\\Security\\MicrosoftVolumeEncryption";
            string wmiQuery = $"SELECT * FROM Win32_EncryptableVolume WHERE DriveLetter = '{driveLetter}'";

            using (var searcher = new ManagementObjectSearcher(wmiNamespace, wmiQuery))
            using (var results = searcher.Get())
            {
                if (results.Count == 0) return 0;

                foreach (ManagementObject obj in results)
                {
                    uint status = (uint)obj.InvokeMethod("GetProtectionStatus", null)["ReturnValue"];
                    return (int)status;
                }
            }
        }
        catch (Exception) { return -1; }
        return 0;
    }
}
