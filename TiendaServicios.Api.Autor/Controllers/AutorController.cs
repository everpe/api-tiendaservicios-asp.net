using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TiendaServicios.Api.Autor.Aplicacion;
using TiendaServicios.Api.Autor.Modelo;

namespace TiendaServicios.Api.Autor.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AutorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AutorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //Recibe la petición y manda la data a la Clase intermedia Nuevo(mediatR) Creación.
        [HttpPost]
        public async Task<ActionResult<Unit>> CrearAutor(Nuevo.Ejecuta data)
        {
            return await _mediator.Send(data);
        }

        //Lista de autores
        [HttpGet]
        public async Task<ActionResult<List<AutorDto>>> GetAutores() {
            return await _mediator.Send(new Consulta.ListaAutor());
        }

        //GetAutor ById
        [HttpGet("{id}")]
        public async Task<ActionResult<AutorDto>> GetAutorById(string id) {
            return await _mediator.Send(new ConsultaFiltro.AutorUnico { AutorGuid = id });
        }

    }
}
