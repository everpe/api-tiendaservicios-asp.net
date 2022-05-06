using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaServicios.Api.CarritoCompra.Persistencia;
using TiendaServicios.Api.CarritoCompra.RemoteInterface;

namespace TiendaServicios.Api.CarritoCompra.Aplicacion
{
    public class Consulta
    {
        public class Ejecuta : IRequest<CarritoDto>{
            public int CarritoSessionId { get; set; }

            public class Manejador : IRequestHandler<Ejecuta, CarritoDto>
            {
                private readonly ContextoBd _contexto;
                //Serivicio que implementa la consulta a librosMicroservice
                private readonly ILibroService _libroService;
                private readonly ILogger _logger;
                public Manejador(ContextoBd contexto, ILibroService libroService, ILogger logger)
                {
                    _contexto = contexto;
                    _libroService = libroService;
                    _logger = logger;
                }
                public async Task<CarritoDto> Handle(Ejecuta request, CancellationToken cancellationToken)
                {
                    var carritoSesionBd = await _contexto.CarritoSesionTable.FirstOrDefaultAsync( 
                        car => car.CarritoSesionId == request.CarritoSessionId
                    );
                    var carsSesionDetalle = await _contexto.CarritoSesiondetalleTable.Where(
                        carDet => carDet.CarritoSesionId == request.CarritoSessionId    
                    ).ToListAsync();


                    //Lista de Libros para CarritoDto se llena de la Consulta a LibroMicroservice
                    var listadoCarritoDto = new List<CarritoDetalleDto>();
                    foreach(var libro in carsSesionDetalle)
                    {
                        var responseLibro = await _libroService.GetLibro(new Guid(libro.ProductoSeleccionado));
                        _logger.LogInformation("Hola Mundo Cruell:::");
                        if (responseLibro.resultado)
                        {
                            var objetoLibro = responseLibro.libro;
                            _logger.LogInformation("Hola1 Mundo Cruell:::");
                            _logger.LogInformation("Hola2 Mundo Cruell:::");
                            _logger.LogInformation("Hola3 Mundo Cruell:::");
                            var carritoDetalleDto = new CarritoDetalleDto
                            {
                                TituloLibro = objetoLibro.Titulo,
                                FechaPublicacion = objetoLibro.FechaPublicacion,
                                LibroId = objetoLibro.LibreriaMaterialId,
                            };
                            listadoCarritoDto.Add(carritoDetalleDto);
                        }
                    }

                    //Objeto Final armado con toda la Info neesaria
                    var carritoSesionDto = new CarritoDto
                    {
                        CarritoId = carritoSesionBd.CarritoSesionId,
                        FechaCreacionSesion = carritoSesionBd.FechaCreacion,
                        ListaProductos = listadoCarritoDto,
                    };
                    return carritoSesionDto;
                }
            }
        }
    }
}
