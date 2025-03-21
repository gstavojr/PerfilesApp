using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PerfilesApp.Models;


namespace Perfiles.AccesoDatos.IRepository
{
  interface IDepartamentoRepository
  {
    Response<IEnumerable<Departamento>> GetDepartamentos();
    Response<Departamento> GetDepartamento(int id);
    Response<bool> CreateDepartamento(Departamento departamento);
    Response<bool> UpdateDepartamento(Departamento newDepartamento);
    Response<bool> DeleteDepartamento(int id);
    bool Save();
  }
}
