using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfilesApp.AccesoDatos.IRepository
{
  interface IObjectService<T>
  {
    Task<IEnumerable<T>> GetObjects();
    Task<T> GetObject(int id);

    Task<bool> Save(T objetValue);
  }
}
