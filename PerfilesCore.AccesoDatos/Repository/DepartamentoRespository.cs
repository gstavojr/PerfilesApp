using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Perfiles.AccesoDatos.Data;
using Perfiles.AccesoDatos.IRepository;
using PerfilesModels;

namespace Perfiles.AccesoDatos.Repository
{
  public class DepartamentoRespository : IDepartamentoRepository
  {
    private readonly Conection conection;
    public DepartamentoRespository()
    {
      this.conection = new Conection();
    }
    public bool CreateDepartamento(Departamento departamento)
    {
      throw new NotImplementedException();
    }

    public bool DeleteDepartamento(int id)
    {
      throw new NotImplementedException();
    }

    public async Task<Departamento> GetDepartamento(int id)
    {
      try
      {
        Departamento? departamento = await this.conection.ExecuteQuery("EXEC sp_GetDepartamentoById @Id;", async reader =>
        {
          if (await reader.ReadAsync())
          {
            return new Departamento
            {
              DepartamentoId = Convert.ToInt32(reader["DepartamentoId"]),
              Nombre = reader["Nombre"]?.ToString() ?? string.Empty,
              Estado = Convert.ToInt32(reader["Estado"])
            };
          }
          return null; // Si no se encuentra el departamento
        }, new SqlParameter("@Id", id));

        return departamento;
      }
      catch (Exception)
      {
        return null;
      }
      


    }

    public async Task<IEnumerable<Departamento>> GetDepartamentos()
    {
      IEnumerable<Departamento> departamentos = [];

      try
      {
        departamentos = await conection.ExecuteQuery("EXEC sp_GetDepartamentos;", async reader =>
        {
          ICollection<Departamento> departamentosTemp = [];

          while (await reader.ReadAsync())
          {
            departamentosTemp.Add(new Departamento
            {
              DepartamentoId = (int)reader["DepartamentoId"],
              Nombre = reader["Nombre"].ToString(),
              Estado = (int)reader["Estado"]
            });
          }
          return departamentosTemp;

        });

        return departamentos;

      }
      catch (Exception)
      {
        return [];
      }
    }

    public bool Save()
    {
      throw new NotImplementedException();
    }

    public bool UpdateDepartamento(Departamento newDepartamento)
    {
      throw new NotImplementedException();
    }
  }
}
