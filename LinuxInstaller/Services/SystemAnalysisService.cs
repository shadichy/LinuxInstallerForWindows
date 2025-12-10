using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace LinuxInstaller.Services;

public sealed partial class BitLockerStatus : ObservableObject
{
    public required string Drive { get; set; }
    public required int Status { get; set; }
}


// TODO: Replace all placeholder logic in this service with real system calls (e.g., WMI, P/Invoke).
public class SystemAnalysisService
{
    public async Task<bool> IsRunningAsAdmin()
    {
        // TODO: Implement a real check for administrator privileges.
        // The original implementation from PreFlightCheckViewModel can be used here.
        return true;
    }

    public async Task RelaunchAsAdmin()
    {
        // TODO: Implement the logic to relaunch the application with administrator privileges.
        // The original implementation from PreFlightCheckViewModel can be used here.
        return;
    }

    private readonly ObservableCollection<BitLockerStatus> _bitLockerStatuses = [];

    public async Task InitializeBitLockerStatus(string driveLetter = "C:")
    {
        // TODO: Implement a real WMI call to get BitLocker status for the given drive.
        // The original implementation from PreFlightCheckViewModel can be used here.
        // Placeholder: 0=Unencrypted, 1=Encrypted (Protection On), 2=Encrypted (Protection Off)
        _bitLockerStatuses.Add(new() { Drive = driveLetter, Status = 0 });
        return;
    }


    public async Task<int> GetBitLockerStatus(string driveLetter = "C:")
    {
        IEnumerable<BitLockerStatus> _status = _bitLockerStatuses.Where((obj) => obj.Drive == driveLetter);
        if (_status.Any())
        {
            return _status.First().Status;
        }
        else
        {
            await InitializeBitLockerStatus(driveLetter);
            return await GetBitLockerStatus(driveLetter);
        }
    }

    private string _bootMode = string.Empty;

    public async Task InitializeBootMode()
    {
        // TODO: Implement a real check for the system's boot mode (UEFI or Legacy BIOS).
        _bootMode = "UEFI";
        return;
    }


    public async Task<string> GetBootMode()
    {
        if (string.IsNullOrWhiteSpace(_bootMode))
        {
            await InitializeBootMode();
        }
        return _bootMode;
    }

    private bool _isSecureBootEnabled = false;

    public async Task InitializeSecureBootStatus()
    {
        // TODO: Implement a real check for the Secure Boot status.
        _isSecureBootEnabled = true;
        return;
    }


    public async Task<bool> GetSecureBootStatus()
    {
        if (!_isSecureBootEnabled)
        {
            await InitializeSecureBootStatus();
        }
        return _isSecureBootEnabled;
    }
}

