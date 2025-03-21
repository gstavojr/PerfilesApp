using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PerfilesServer.Models;
using PerfilesServer.Models.Dto;
using PerfilesServer.Respository.IRepository;

namespace PerfilesServer.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class EmpleadoController : ControllerBase
  {

    private readonly IEmpleadoRespository empleadoRespository;
    private readonly IMapper mapper;
    private readonly ILogger logger;

    public EmpleadoController(IEmpleadoRespository empleadoRespository, IMapper mapper, ILogger<EmpleadoController> logger)
    {
      this.empleadoRespository = empleadoRespository;
      this.mapper = mapper;
      this.logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetEmpleados()
    {
      this.logger.LogInformation("Obteniendo empleados");
      var responseEmpleado = await this.empleadoRespository.GetEmpleados();
      return Ok(responseEmpleado.Data);
    }

    [HttpGet("{identificador:int}", Name = "GetEmpleado")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetEmpleado(int identificador)
    {
      this.logger.LogInformation("Obteniendo empleado");
      var response = await this.empleadoRespository.GetEmpleado(identificador);
      if (!response.IsSucces)
      {
        this.logger.LogWarning(response.Message);
        return NotFound(response.Message);
      }
      return Ok(response.Data);
    }

    [HttpPost]
    [ProducesResponseType(201, Type = typeof(CrearEmpleadoDto))]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]

    public async Task<IActionResult> SaveEmpleado([FromBody] CrearEmpleadoDto empleadoDto)
    {
      this.logger.LogInformation("Guardando empleado");
      if (empleadoDto == null)
      {
        this.logger.LogWarning("Empleado es nulo");
        return BadRequest(ModelState);
      }
      if (!ModelState.IsValid)
      {
        var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
        var errosString = string.Join(",", errors);

        this.logger.LogWarning(errosString);
        return BadRequest(ModelState);
      }

      var empleado = this.mapper.Map<Empleado>(empleadoDto);
      var response = await this.empleadoRespository.SaveEmpleado(empleado);
      
      if (!response.IsSucces)
      {
        this.logger.LogWarning($"Error al guardar {empleado.Nombre}; ${response.Message}");
        return StatusCode(500, $"Error al guardar {empleado.Nombre}; ${response.Message}");
      }
      return Ok(empleadoDto);
    }


    [HttpPut("{identificador:int}", Name = "UpdateEmpleado")]
    [ProducesResponseType(201, Type = typeof(CrearEmpleadoDto))]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> UpdateEmpleado(int identificador, [FromBody] ActualizarEmpleadoDto empleadoDto)
    {
      this.logger.LogInformation("Actualizando empleado");
      if (empleadoDto == null || identificador != empleadoDto.EmpleadoId)
      {
        this.logger.LogWarning("Empleado es nulo");
        return BadRequest(ModelState);
      }
      if (!ModelState.IsValid)
      {
        var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
        var errosString = string.Join(",", errors);

        this.logger.LogWarning(errosString);
        return BadRequest(ModelState);
      }
      var empleado = this.mapper.Map<Empleado>(empleadoDto);
      var response = await this.empleadoRespository.SaveEmpleado(empleado);
      if (!response.IsSucces)
      {
        this.logger.LogWarning($"Error al guardar {empleado.Nombre}; ${response.Message}");
        return StatusCode(500, $"Error al guardar {empleado.Nombre}; ${response.Message}");
        
      }

      return Ok(empleadoDto);
    }

    [HttpDelete("{identificador:int}", Name = "DeleteEmpleado")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteEmpleado(int identificador)
    {
      this.logger.LogInformation("Eliminando empleado");
      var response = await this.empleadoRespository.DeleteEmpleado(identificador);
      if (!response.IsSucces)
      {
        this.logger.LogWarning(response.Message);
        return NotFound(response.Message);
      }
      return Ok();
    }



  }
}
