using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PerfilesApp.Models;

namespace PerfilesApp.AccesoDatos.IRepository
{
  interface IObjectService<T>
  {
    Task<Response<IEnumerable<T>>> GetObjects();
    Task<Response<T>> GetObject(int id);
    Task<Response<bool>> Delete(int id);
    Task<Response<bool>> Save(T objetValue);
  }
}
