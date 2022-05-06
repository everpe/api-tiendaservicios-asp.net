namespace TiendaServicios.Api.CarritoCompra.RemoteModels
{
    //Sirve para modelar la Data Traida desde el microservice de Libro(HttClient)
    public class LibroRemote
    {
        //Campos Modelo Libro de MicroserviceLibro
        public Guid? LibreriaMaterialId { get; set; }
        public string Titulo { get; set; }
        public DateTime? FechaPublicacion { get; set; }
        public Guid? AutorLibro { get; set; }//Referencia a idAutorLibro microservice de Libro 
    }

}
