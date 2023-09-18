using System.Net.Http;
using System.Threading.Tasks;

namespace _1p_atom_carmanager.simulator.app;

public class ApiService
{
    private static readonly HttpClient client = new HttpClient();
    private static readonly string baseUrl = "http://atomkai1p-001-site1.dtempurl.com/";

    public static async Task<string> Get(string endpoint)
    {
        string result = null;
        HttpResponseMessage response = await client.GetAsync(baseUrl + endpoint);
        if (response.IsSuccessStatusCode)
        {
            result = await response.Content.ReadAsStringAsync();
        }
        return result;
    }

    public static async Task<string> Post(string endpoint, string payload)
    {
        string result = null;
        HttpResponseMessage response = await client.PostAsync(baseUrl + endpoint, new StringContent(payload));
        if (response.IsSuccessStatusCode)
        {
            result = await response.Content.ReadAsStringAsync();
        }
        return result;
    }

    public static async Task<bool> Delete(string endpoint)
    {
        HttpResponseMessage response = await client.DeleteAsync(baseUrl + endpoint);
        return response.IsSuccessStatusCode;
    }

    public static async Task<bool> Put(string endpoint, string payload)
    {
        HttpResponseMessage response = await client.PutAsync(baseUrl + endpoint, new StringContent(payload));
        return response.IsSuccessStatusCode;
    }
}

