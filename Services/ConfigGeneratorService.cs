using LinuxInstaller.Models;
using System.Collections.Generic;

namespace LinuxInstaller.Services;

public class ConfigGeneratorService
{
    public string GenerateGrubStage1Config(string stage2MarkerPath)
    {
        // Placeholder: Return example grub.cfg content
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
        // Placeholder: Return example grub.cfg content for stage 2
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
        // Placeholder: Return example install.conf JSON content
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
