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
    public Response<bool> CreateDepartamento(Departamento departamento)
    {
      return this.SaveDepartamento(departamento);
    }



    public Response<bool> DeleteDepartamento(int id)
    {
      var departamentoSearch = this.GetDepartamento(id);

      if (departamentoSearch == null)
      {
        return new Response<bool>(false, "Departamento no existe");
      }

      try
      {

        this.conection.ExecuteNonQuery(
          "EXEC sp_DeleteEmpleadoById @DepartamentoId;", 
          new SqlParameter("@DepartamentoId", id)
        );

        return new Response<bool>(true, "Departamento eliminado correctamente");
      }
      catch (Exception ex)
      {
        // Mesanje de error
        
        return new Response<bool>(false, $"Error al eliminar el departamento: {ex.Message}");

      }
    }

    public Response<Departamento> GetDepartamento(int id)
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

        if (departamento == null)
        {
          return new Response<Departamento>(false, "Departamento no encontrado");
        }

        return new Response<Departamento>(true, "", departamento);
      }
      catch (Exception ex)
      {
        return new Response<Departamento>(false, $"Error al obtener el departamento: {ex.Message}");
      }
      


    }

    public Response<IEnumerable<Departamento>> GetDepartamentos()
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

        return new Response<IEnumerable<Departamento>>(true, "", departamentos);

      }
      catch (Exception ex)
      {
        return new Response<IEnumerable<Departamento>>(false, $"Error al obtener los departamentos: {ex.Message}"); ;
      }
    }

    public bool Save()
    {
      throw new NotImplementedException();
    }

    public Response<bool> UpdateDepartamento(Departamento departamento)
    {
      return this.SaveDepartamento(departamento);
    }

    private Response<bool> SaveDepartamento(Departamento departamento)
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

        return new Response<bool>(true, "Departamento guarado correctamente");
      }
      catch (Exception ex)
      {
        return new Response<bool>(false, $"Error al guardar el departamento: {ex.Message}");
      }

    } 

   

   
  }
}
