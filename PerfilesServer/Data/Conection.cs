using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Perfiles.AccesoDatos.Data
{
  public class Conection
  {
    private readonly string? ConectionString;

    public Conection(IConfiguration configuration)
    {
      ConectionString = configuration.GetConnectionString("DefaultConnection");
    }

    // Método asíncrono para ejecutar consultas con un mapeo de resultados
    public async Task<T> ExecuteQueryAsync<T>(string query, Func<SqlDataReader, T> mapper)
    {
      using (SqlConnection connection = new SqlConnection(ConectionString))
      {
        await connection.OpenAsync();  // Usar OpenAsync en lugar de Open
        using (SqlCommand command = new SqlCommand(query, connection))
        {
          using (SqlDataReader reader = await command.ExecuteReaderAsync())  // Usar ExecuteReaderAsync
          {
            return mapper(reader);
          }
        }
      }
    }

    // Método asíncrono para ejecutar consultas con parámetros y un mapeo de resultados
    public async Task<T> ExecuteQueryAsync<T>(
        string query, Func<SqlDataReader, T> mapper,
        params SqlParameter[] parameters)
    {
      using (SqlConnection connection = new SqlConnection(ConectionString))
      {
        await connection.OpenAsync();  // Usar OpenAsync en lugar de Open
        using (SqlCommand command = new SqlCommand(query, connection))
        {
          if (parameters != null && parameters.Length > 0)
          {
            command.Parameters.AddRange(parameters);
          }
          using (SqlDataReader reader = await command.ExecuteReaderAsync())  // Usar ExecuteReaderAsync
          {
            return mapper(reader);
          }
        }
      }
    }

    // Método asíncrono para ejecutar comandos no query (INSERT, UPDATE, DELETE)
    public async Task ExecuteNonQueryAsync(string query, params SqlParameter[] parameters)
    {
      using (SqlConnection connection = new SqlConnection(ConectionString))
      {
        await connection.OpenAsync();  // Usar OpenAsync en lugar de Open
        using (SqlCommand cmd = new SqlCommand(query, connection))
        {
          // Agregar parámetros si los hay
          if (parameters != null && parameters.Length > 0)
          {
            cmd.Parameters.AddRange(parameters);
          }

          // Ejecutar la consulta de forma asíncrona
          await cmd.ExecuteNonQueryAsync();  // Usar ExecuteNonQueryAsync
        }
      }
    }
  }
}
