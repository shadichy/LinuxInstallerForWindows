using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using LinuxInstaller.Models;

namespace LinuxInstaller.Services;

public class DistroService
{
    private readonly HttpClient _httpClient;
    private readonly string _distrosUrl;

    public DistroService(string distrosUrl)
    {
        _httpClient = new HttpClient();
        _distrosUrl = distrosUrl;
    }

    public async Task<IEnumerable<Distro>> GetDistrosAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<Distro>>(_distrosUrl);
    }
}

