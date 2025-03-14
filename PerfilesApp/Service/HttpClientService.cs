using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace PerfilesApp.Service
{
	public class HttpClientService
	{
		private HttpClient _httpClient;

    public HttpClientService()
    {
      this._httpClient = new HttpClient
      {
        BaseAddress = new Uri("https://localhost:7113/api/")
      };

    }

    public async Task<T> GetAsync<T>(string uri)
    {
      try
      {
        HttpResponseMessage response = await _httpClient.GetAsync(uri);
        response.EnsureSuccessStatusCode();  // Lanza una excepción si el código de estado HTTP no es exitoso

        string jsonResponse = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(jsonResponse);
      }
      catch (Exception ex)
      {
        // Manejo de excepciones (puedes personalizarlo)
        Console.WriteLine($"Error al hacer la petición GET: {ex.Message}");
        return default;
      }
    }

    public async Task<bool> PostAsync(string uri, HttpContent content)
    {
      try
      {
        HttpResponseMessage response = await _httpClient.PostAsync(uri, content);
        return response.IsSuccessStatusCode;
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Error al hacer la petición POST: {ex.Message}");
        return false;
      }
    }

    public async Task<bool> PutAsync(string uri, HttpContent content)
    {
      try
      {
        HttpResponseMessage response = await _httpClient.PutAsync(uri, content);
        return response.IsSuccessStatusCode;
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Error al hacer la petición PUT: {ex.Message}");
        return false;
      }
    }
  }
}