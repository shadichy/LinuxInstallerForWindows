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

    public Task<IEnumerable<Distro>> GetDistrosAsync()
    {
        // TODO: Replace this placeholder with a real HTTP request to fetch the distro list from a remote URL.
        var distros = new List<Distro>
        {
            new Distro { Name = "Ubuntu", Description = "The leading Linux distribution for desktop and server.", Size = 4_000_000_000 },
            new Distro { Name = "Fedora", Description = "A polished, easy to use operating system for laptops and desktops.", Size = 2_000_000_000 },
            new Distro { Name = "Debian", Description = "The Universal Operating System.", Size = 1_200_000_000 },
            new Distro { Name = "Arch Linux", Description = "A lightweight and flexible Linux distribution.", Size = 800_000_000 }
        };
        return Task.FromResult<IEnumerable<Distro>>(distros);
    }
}

