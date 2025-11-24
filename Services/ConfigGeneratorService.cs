using LinuxInstaller.Models;
using System.Collections.Generic;
using System.Text.Json; // Add for future use

namespace LinuxInstaller.Services;

// TODO: Replace all placeholder logic in this service with real config generation based on user input and system state.
public class ConfigGeneratorService
{
    public string GenerateGrubStage1Config(string stage2MarkerPath)
    {
        // TODO: This config is mostly static, but ensure the search path is correct.
        // The placeholder is likely sufficient for most cases.
        return @"
set timeout=5
echo ""Searching for My Linux Installer (Stage 2)...""

# Search all partitions for the Stage 2 marker file
if search --file --no-floppy --set=root /.myinstaller/stage2.cfg ; then
    echo ""Installer found on $root.""
    configfile /.myinstaller/stage2.cfg
else
    echo ""Error: Could not find /.myinstaller/stage2.cfg on any drive.""
    sleep 10
fi
";
    }

    public string GenerateGrubStage2Config(string isoPath)
    {
        // TODO: Dynamically generate this config based on the selected workflow (ISO vs. Automated)
        // and the actual paths/names of kernel/initrd files or ISOs.
        return @"
set timeout=10

menuentry ""Start Automated Install"" {
    # Root is already set to D:
    linux /.myinstaller/installer.vmlinuz
    initrd /.myinstaller/installer.initrd
}

menuentry ""Start Ubuntu 24.04 ISO"" {
    set isofile=""/.myinstaller/ubuntu-24.04.iso""
    loopback loop $isofile
    linux (loop)/casper/vmlinuz boot=casper iso-scan/filename=$isofile ---
    initrd (loop)/casper/initrd
}
";
    }

    public string GenerateInstallConf(string diskGuid, List<PartitionPlan> plan)
    {
        // TODO: Serialize the actual user-defined partition plan and other settings into a JSON string.
        // Example of a real implementation:
        // var config = new {
        //   install_mode = "manual",
        //   target_disk_guid = diskGuid,
        //   rootfs_file = "ubuntu.rootfs",
        //   partition_plan = plan
        // };
        // return JsonSerializer.Serialize(config);

        return @"
{
  ""install_mode"": ""manual"",
  ""target_disk_guid"": ""YOUR-DISK-GUID-HERE"",
  ""rootfs_file"": ""ubuntu.rootfs"",
  ""partition_plan"": [
    { ""mount_point"": ""/"", ""fs_type"": ""ext4"", ""size_mb"": 80000 },
    { ""mount_point"": ""swap"", ""fs_type"": ""linux-swap"", ""size_mb"": 16000 }
  ]
}
";
    }
}
