using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.Data.SqlClient;


namespace Perfiles.AccesoDatos.Data
{
  public class Conection
  {
    public static string ConectionString => ConfigurationManager.ConnectionStrings["PerfilesDB"].ConnectionString;

    public async Task<T> ExecuteQuery<T>(string query, Func<SqlDataReader, Task<T>> mapper)
    {
      using (SqlConnection connection = new SqlConnection(ConectionString))
      {
        await connection.OpenAsync();
        using (SqlCommand command = new SqlCommand(query, connection))
        {
          using (SqlDataReader reader = await command.ExecuteReaderAsync())
          {
            return await mapper(reader);
          }
        }
      }
    }

    public async Task<T> ExecuteQuery<T>(
      string query, Func<SqlDataReader,
      Task<T>> mapper,
      params SqlParameter[] parameters
    )
    {
      using (SqlConnection connection = new SqlConnection(ConectionString))
      {
        await connection.OpenAsync();
        using (SqlCommand command = new SqlCommand(query, connection))
        {
          if (parameters != null && parameters.Length > 0)
          {
            command.Parameters.AddRange(parameters);
          }
          using (SqlDataReader reader = await command.ExecuteReaderAsync())
          {
            return await mapper(reader);
          }
        }
      }
    }

    public async Task ExecuteNonQueryAsync(string query, params SqlParameter[] parameters)
    {
      using (SqlConnection connection = new SqlConnection(ConectionString))
      {
        await connection.OpenAsync();
        using (SqlCommand cmd = new SqlCommand(query, connection))
        {
          // Agregar parámetros si los hay
          if (parameters != null && parameters.Length > 0)
          {
            cmd.Parameters.AddRange(parameters);
          }

          // Ejecutar la consulta
          await cmd.ExecuteNonQueryAsync();
        }
      }
    }
  }
}
