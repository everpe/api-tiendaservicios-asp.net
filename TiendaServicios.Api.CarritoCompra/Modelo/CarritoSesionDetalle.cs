namespace TiendaServicios.Api.CarritoCompra.Modelo
{
    public class CarritoSesionDetalle
    {
        public int CarritoSesionDetalleId { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string ProductoSeleccionado { get; set; }//nombreProducto
        
        //Clave Foranea de CarritoSesion 1:N
        public int CarritoSesionId { get; set; }
        //Ancla para Relacion de Objetos
        public CarritoSesion CarritoSesion { get; set; }
    }
}
