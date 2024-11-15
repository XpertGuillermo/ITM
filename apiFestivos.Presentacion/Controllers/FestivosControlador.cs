using apiFestivos.Core.Interfaces.Servicios;
using apiFestivos.Dominio.DTOs;
using apiFestivos.Dominio.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace apiFestivos.Presentacion.Controllers
{
    [ApiController]
    [Route("api/festivos")]
    public class FestivosControlador : ControllerBase
    {
        private readonly IFestivoServicio servicio;

        public FestivosControlador(IFestivoServicio servicio)
        {
            this.servicio = servicio;
        }

        [HttpGet("listar")]
        public async Task<ActionResult<IEnumerable<Festivo>>> ObtenerTodos()
        {
            return Ok(await servicio.ObtenerTodos());
        }

        [HttpGet("obtener/{Id}")]
        public async Task<ActionResult<Festivo>> Obtener(int Id)
        {
            return Ok(await servicio.Obtener(Id));
        }

        [HttpGet("buscar/{Dato}")]
        public async Task<ActionResult<Festivo>> Buscar(string Dato)
        {
            return Ok(await servicio.Buscar(Dato));
        }

        [HttpPost("agregar")]
        public async Task<ActionResult<Festivo>> Agregar([FromBody] Festivo Festivo)
        {
            return Ok(await servicio.Agregar(Festivo));
        }

        [HttpPut("modificar")]
        public async Task<ActionResult<Festivo>> Modificar([FromBody] Festivo Festivo)
        {
            return Ok(await servicio.Modificar(Festivo));
        }

        [HttpDelete("eliminar/{Id}")]
        public async Task<ActionResult<bool>> Eliminar(int Id)
        {
            return Ok(await servicio.Eliminar(Id));
        }

        //********** Consultas //**********

        [HttpGet("listar/{Year}")]
        public async Task<ActionResult<IEnumerable<FechaFestivo>>> ObtenerAño(int Year)
        {
            return Ok(await servicio.ObtenerAño(Year));
        }

        [HttpGet("Verificar/{Year}/{Mes}/{Dia}")]
        public async Task<ActionResult<bool>> EsFestivo(int Year, int Mes, int Dia)
        {
            // Validar que la fecha sea válida
            if (!DateTime.TryParse($"{Year}-{Mes}-{Dia}", out DateTime fecha))
            {
                return BadRequest("La fecha proporcionada no es válida.");
            }

            return Ok(await servicio.EsFestivo(fecha));
        }

    }
}
