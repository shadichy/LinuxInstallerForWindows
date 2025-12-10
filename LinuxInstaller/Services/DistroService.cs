using System.Collections.Generic;
using System.Threading.Tasks;
using LinuxInstaller.Models;

namespace LinuxInstaller.Services;

public class DistroService
{
    public DistroService()
    {
        // TODO: In a real implementation, this constructor might take an HttpClient or a config object
        // with the URL for the distro list.
    }

    public Task<IEnumerable<Distro>> GetDistros()
    {
        // TODO: Replace this placeholder with a real HTTP request to fetch the distro list from a remote URL.
        var distros = new List<Distro>
        {
            new()
            {
                DistroName = "Ubuntu",
                Version = "24.04 LTS",
                Description = "The world's most popular open-source OS.",
                Size = 5_100_000_000,
                DownloadUrl = "https://example.com/download/ubuntu-24.04-lts.rootfs",
                IconUrl = "https://distrowatch.com/images/yvzhuwbpy/ubuntu.png"
            },
            new()
            {
                DistroName = "Fedora",
                Version = "40",
                Description = "A leading-edge, free and open source operating system.",
                Size = 4_200_000_000,
                DownloadUrl = "https://example.com/download/fedora-40.rootfs",
                IconUrl = "https://distrowatch.com/images/yvzhuwbpy/fedora.png"
            },
            new()
            {
                DistroName = "Debian",
                Version = "12",
                Description = "The universal operating system, known for its stability.",
                Size = 3_800_000_000,
                DownloadUrl = "https://example.com/download/debian-12.rootfs",
                IconUrl = "https://distrowatch.com/images/yvzhuwbpy/debian.png"
            },
            new()
            {
                DistroName = "Manjaro",
                Version = "23",
                Description = "A professional, user-friendly, Arch-based distribution.",
                Size = 4_900_000_000,
                DownloadUrl = "https://example.com/download/manjaro-23.rootfs",
                IconUrl = "https://distrowatch.com/images/yvzhuwbpy/arch.png"
            },
            new()
            {
                DistroName = "Arch Linux",
                Version = "",
                Description = "A lightweight and flexible Linux distribution.",
                Size = 1_100_000_000,
                DownloadUrl = "https://example.com/download/arch-linux.rootfs",
                IconUrl = "https://distrowatch.com/images/yvzhuwbpy/manjaro.png"
            }
        };
        return Task.FromResult<IEnumerable<Distro>>(distros);
    }
}

