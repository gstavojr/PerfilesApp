using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using PerfilesApp.AccesoDatos.IRepository;
using PerfilesApp.Models;

namespace PerfilesApp.Service
{
  public class EmpleadoService: IObjectService<Empleado>
  {
    private HttpClientService http;
    public EmpleadoService()
    {
      this.http = new HttpClientService();
    }

    public async Task<Response<Empleado>> GetObject(int id)
    {
      return await this.http.GetAsync<Empleado>($"Empleado/{id}");
      
    }

    public async Task<Response<IEnumerable<Empleado>>> GetObjects()
    {
      var empleadosList = await this.http.GetAsync<IEnumerable<Empleado>>("Empleado");
      return empleadosList;
    }

    public async Task<Response<bool>> Save(Empleado objetValue)
    {
      string json = JsonConvert.SerializeObject(objetValue);
      HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

      if (objetValue.EmpleadoId == 0 )
      {
        return await this.http.PostAsync("Empleado", content);
        
      }
      return await this.http.PutAsync($"Empleado/{objetValue.EmpleadoId}", content);

    }

    public async Task<Response<bool>> Delete(int id)
    {
      return await this.http.DeleteAsync($"Empleado/{id}");
    }


  }
}