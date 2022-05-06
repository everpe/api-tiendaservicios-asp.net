namespace TiendaServicios.Api.Autor.Aplicacion
{
    //Mapeo DTo de clase AutorLibro
    public class AutorDto
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime? FechaNacimiento { get; set; }


        public string AutorLibroGuid { get; set; }//Global unique identifier

    }
}
