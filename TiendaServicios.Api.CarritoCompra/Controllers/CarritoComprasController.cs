using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TiendaServicios.Api.CarritoCompra.Aplicacion;

namespace TiendaServicios.Api.CarritoCompra.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarritoComprasController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CarritoComprasController(IMediator mediator) {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<ActionResult<Unit>> crear(Nuevo.Ejecuta data)
        {
           return await _mediator.Send(data);
        }


        //Para consumir MicroserviceLibro
        [HttpGet("{id}")]
        public async Task<ActionResult<CarritoDto>> GetCarritoWihtDetalleLibro(int id)
        {
            return await _mediator.Send(new Consulta.Ejecuta { CarritoSessionId = id });
        }

    }
}
