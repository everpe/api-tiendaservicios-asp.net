using System;

namespace TiendaServicios.Api.Autor.Modelo
{
    public class GradoAcademico
    {
        public int GradoAcademicoId { get; set; }
        public string Nombre { get; set; }

        public string CentroAcademico { get; set; }

        public DateTime? FechaGrado { get; set; }

        //Relación(1:N) Autor al que pertenece el grado
        public int AutorLibroId { get; set; }
        public AutorLibro AutorLibro { get; set; }

        //Valor universal unico para cuando seguir o transmitir una Instancia
        public string GradoAcademicoGuid { get; set; }//Global unique identifier
    }
}
