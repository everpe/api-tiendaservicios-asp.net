using MediatR;
using TiendaServicios.Api.CarritoCompra.Modelo;
using TiendaServicios.Api.CarritoCompra.Persistencia;

namespace TiendaServicios.Api.CarritoCompra.Aplicacion
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
            //Para crear SesionCompra
            public DateTime FechaCreacionSesion { get; set; }


            //Para SesionDetalleCompra
            public List<string> ProductosListaDetalle { get; set;}//Lista de Ids de Libros a insertar en tablaDetalle
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly ContextoBd _context;
            public Manejador(ContextoBd contexto)
            {
                _context = contexto;
            }
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                //CREACIÓN CARRITO_COMPRA
                var carritoSesion = new CarritoSesion { 
                    FechaCreacion = request.FechaCreacionSesion
                };
                _context.CarritoSesionTable.Add(carritoSesion);
                var insertion = await _context.SaveChangesAsync();
                if (insertion == 0)
                {
                    throw new Exception("Error insertando CarritoCompra");
                }

                int idCarritoSaved = carritoSesion.CarritoSesionId;

                
                //CREACION CarritoCompraDetalle
                foreach(var nameProduct in request.ProductosListaDetalle)
                {
                    var detalleCarrito = new CarritoSesionDetalle
                    {
                        FechaCreacion = DateTime.Now,
                        CarritoSesionId = idCarritoSaved,
                        ProductoSeleccionado = nameProduct,
                    };
                    _context.CarritoSesiondetalleTable.Add(detalleCarrito);
                }
                insertion = await _context.SaveChangesAsync();
                if (insertion > 0)
                {
                    return Unit.Value;
                }
                throw new Exception("No se pudó insertar los detalle de compra");
            }
        }
    }
}
