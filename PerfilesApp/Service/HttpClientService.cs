using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using PerfilesApp.Models;

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

    public async Task<Response<T>> GetAsync<T>(string uri)
    {
      try
      {
        HttpResponseMessage response = await _httpClient.GetAsync(uri);
        response.EnsureSuccessStatusCode();  // Lanza una excepción si el código de estado HTTP no es exitoso

        string jsonResponse = await response.Content.ReadAsStringAsync();
        return new Response<T>(true, "", JsonConvert.DeserializeObject<T>(jsonResponse));
      }
      catch (Exception ex)
      {
        // Manejo de excepciones (puedes personalizarlo)
        Console.WriteLine($"Error al hacer la petición GET: {ex.Message}");
        return new Response<T>(false, $"Error al hacer la petición GET: {ex.Message}");
      }
    }

    public async Task<Response<bool>> PostAsync(string uri, HttpContent content)
    {
      try
      {
        HttpResponseMessage response = await _httpClient.PostAsync(uri, content);
        var res =  response.IsSuccessStatusCode;
        return new Response<bool>(true, "", res);
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Error al hacer la petición POST: {ex.Message}");
        return new Response<bool>(false, $"Error al hacer la petición POST: {ex.Message}");
      }
    }

    public async Task<Response<bool>> PutAsync(string uri, HttpContent content)
    {
      try
      {
        HttpResponseMessage response = await _httpClient.PutAsync(uri, content);
        return new Response<bool>(true, "", response.IsSuccessStatusCode);
      }
      catch (Exception ex)
      {
        return new Response<bool>(false, $"Error al hacer la petición PUT: {ex.Message}");
      }
    }

    public async Task<Response<bool>> DeleteAsync(string uri)
    {
      try
      {
        HttpResponseMessage response = await _httpClient.DeleteAsync(uri);
        return new Response<bool>(true, "", response.IsSuccessStatusCode);
      }
      catch (Exception ex)
      {
        return new Response<bool>(false, $"Error al hacer la petición DELETE: {ex.Message}");
      }
    }


  }
}