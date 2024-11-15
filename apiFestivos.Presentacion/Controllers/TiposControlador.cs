using apiFestivos.Core.Interfaces.Servicios;
using apiFestivos.Dominio.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace apiFestivos.Presentacion.Controllers
{
    [ApiController]
    [Route("api/tipos")]
    public class TiposControlador : ControllerBase
    {
        private readonly ITipoServicio servicio;

        public TiposControlador(ITipoServicio servicio)
        {
            this.servicio = servicio;
        }

        [HttpGet("listar")]
        public async Task<ActionResult<IEnumerable<Tipo>>> ObtenerTodos()
        {
            return Ok(await servicio.ObtenerTodos());
        }

        [HttpGet("obtener/{Id}")]
        public async Task<ActionResult<Tipo>> Obtener(int Id)
        {
            return Ok(await servicio.Obtener(Id));
        }

        [HttpGet("buscar/{Tipo}/{Dato}")]
        public async Task<ActionResult<Tipo>> Buscar(string Dato)
        {
            return Ok(await servicio.Buscar(Dato));
        }

        [HttpPost("agregar")]
        public async Task<ActionResult<Tipo>> Agregar([FromBody] Tipo Tipo)
        {
            return Ok(await servicio.Agregar(Tipo));
        }

        [HttpPut("modificar")]
        public async Task<ActionResult<Tipo>> Modificar([FromBody] Tipo Tipo)
        {
            return Ok(await servicio.Modificar(Tipo));
        }

        [HttpDelete("eliminar/{Id}")]
        public async Task<ActionResult<bool>> Eliminar(int Id)
        {
            return Ok(await servicio.Eliminar(Id));
        }
    }

}
