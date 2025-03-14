using AutoMapper;
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

    public EmpleadoController(IEmpleadoRespository empleadoRespository, IMapper mapper)
    {
      this.empleadoRespository = empleadoRespository;
      this.mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetEmpleados()
    {

      var empleadosList = await this.empleadoRespository.GetEmpleados();
      return Ok(empleadosList);
    }

    [HttpGet("{identificador:int}", Name = "GetEmpleado")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetEmpleado(int identificador)
    {
      var empleado = await this.empleadoRespository.GetEmpleado(identificador);
      if (empleado == null)
      {
        return NotFound();
      }
      return Ok(empleado);
    }

    [HttpPost]
    [ProducesResponseType(201, Type = typeof(CrearEmpleadoDto))]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]

    public async Task<IActionResult> SaveEmpleado([FromBody] CrearEmpleadoDto empleadoDto)
    {
      if (empleadoDto == null)
      {
        return BadRequest(ModelState);
      }
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var empleado = this.mapper.Map<Empleado>(empleadoDto);
      var success = await this.empleadoRespository.SaveEmpleado(empleado);
      if (!success)
      {
        ModelState.AddModelError("", $"Algo salió mal al guardar el registro {empleado.Nombre}");
        return StatusCode(500, ModelState);
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
      if (empleadoDto == null || identificador != empleadoDto.EmpleadoId)
      {
        return BadRequest(ModelState);
      }
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      var empleado = this.mapper.Map<Empleado>(empleadoDto);
      var success = await this.empleadoRespository.SaveEmpleado(empleado);
      if (!success)
      {
        ModelState.AddModelError("", $"Algo salió mal al guardar el registro {empleado.Nombre}");
        return StatusCode(500, ModelState);
      }
      return Ok(empleadoDto);
    }



  }
}
