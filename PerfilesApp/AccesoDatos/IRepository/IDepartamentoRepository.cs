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
    IEnumerable<Departamento> GetDepartamentos();
    Departamento GetDepartamento(int id);
    bool CreateDepartamento(Departamento departamento);
    bool UpdateDepartamento(Departamento newDepartamento);
    bool DeleteDepartamento(int id);
    bool Save();
  }
}
