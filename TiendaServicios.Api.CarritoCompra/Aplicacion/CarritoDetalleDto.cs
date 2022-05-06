namespace TiendaServicios.Api.CarritoCompra.Aplicacion
{
    //Dto para union de Datos entre distintos microservicios.
    public class CarritoDetalleDto
    {
        public Guid? LibroId { get; set; }
        public string TituloLibro { get; set; }
        public string AutorLibro { get; set; }
        public DateTime? FechaPublicacion { get; set; }
    }
}
