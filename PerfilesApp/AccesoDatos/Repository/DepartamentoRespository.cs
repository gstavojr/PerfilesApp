using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Perfiles.AccesoDatos.Data;
using Perfiles.AccesoDatos.IRepository;
using PerfilesApp.Models;


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
      return this.SaveDepartamento(departamento);
    }



    public bool DeleteDepartamento(int id)
    {
      var departamentoSearch = this.GetDepartamento(id);

      if (departamentoSearch == null)
      {
        return false;
      }

      try
      {

        this.conection.ExecuteNonQuery(
          "DELETE FROM Departamento WHERE DepartamentoId = @DepartamentoId", 
          new SqlParameter("@DepartamentoId", id)
        );

        return true;
      }
      catch (Exception ex)
      {
        return false;

      }
    }

    public Departamento GetDepartamento(int id)
    {
      try
      {
        // @Id 
        Departamento departamento =  this.conection.ExecuteQuery("EXEC sp_GetDepartamentoById @DepartamentoId;", reader =>
        {
          if (reader.Read())
          {
            return new Departamento
            {
              DepartamentoId = Convert.ToInt32(reader["DepartamentoId"]),
              Nombre = reader["Nombre"]?.ToString() ?? string.Empty,
              Estado = Convert.ToInt32(reader["Estado"]),
              NombreEstado = reader["NombreEstado"]?.ToString() ?? string.Empty
            };
          }
          return null; // Si no se encuentra el departamento
        }, new SqlParameter("@DepartamentoId", id));

        return departamento;
      }
      catch (Exception)
      {
        return null;
      }
      


    }

    public IEnumerable<Departamento> GetDepartamentos()
    {
      IEnumerable<Departamento> departamentos = new List<Departamento>();

      try
      {
        departamentos = conection.ExecuteQuery("EXEC sp_GetDepartamentos;",  reader =>
        {
          ICollection<Departamento> departamentosTemp = new List<Departamento>();

          while (reader.Read())
          {
            departamentosTemp.Add(new Departamento
            {
              DepartamentoId = (int)reader["DepartamentoId"],
              Nombre = reader["Nombre"].ToString(),
              Estado = (int)reader["Estado"],
              NombreEstado = reader["NombreEstado"]?.ToString() ?? string.Empty
            });
          }
          return departamentosTemp;

        });

        return departamentos;

      }
      catch (Exception ex)
      {
        return departamentos;
      }
    }

    public bool Save()
    {
      throw new NotImplementedException();
    }

    public bool UpdateDepartamento(Departamento departamento)
    {
      return this.SaveDepartamento(departamento);
    }

    private bool SaveDepartamento(Departamento departamento)
    {

      try
      {
        SqlParameter[] parameters =
        {
          new SqlParameter("@Nombre", departamento.Nombre),
          new SqlParameter("@Estado", departamento.Estado),
          new SqlParameter("@DepartamentoId", departamento.DepartamentoId)
        };
        this.conection.ExecuteNonQuery(
          "EXEC sp_SaveDepartamento @DepartamentoId, @Nombre, @Estado",
          parameters
        );

        return true;
      }
      catch (Exception ex)
      {
        return false;
      }

    } 

   

   
  }
}
