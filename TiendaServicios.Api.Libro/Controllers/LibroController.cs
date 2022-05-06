using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TiendaServicios.Api.Libro.Aplicacion;

namespace TiendaServicios.Api.Libro.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LibroController : ControllerBase
    {

        private readonly IMediator _mediator;
        public LibroController(IMediator mediator) {
            _mediator = mediator;   
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> CreateBook(Nuevo.Ejecuta data) {
            return await _mediator.Send(data);
        }

        [HttpGet]
        public async Task<List<LibreriaMaterialDto>> GetBooks() {
            return await _mediator.Send(new Consulta.Ejecuta());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LibreriaMaterialDto>> GetBookById(Guid id)
        {
            return await _mediator.Send(new ConsultaFiltro.LibroUnico { LibroId = id });
        }
    }
}
