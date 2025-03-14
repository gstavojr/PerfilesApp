using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PerfilesModels;

namespace Perfiles.AccesoDatos.IRepository
{
  interface IDepartamentoRepository
  {
    Task<IEnumerable<Departamento>> GetDepartamentos();
    Task<Departamento> GetDepartamento(int id);
    bool CreateDepartamento(Departamento departamento);
    bool UpdateDepartamento(Departamento newDepartamento);
    bool DeleteDepartamento(int id);
    bool Save();
  }
}
