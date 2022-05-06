using System;
using System.Collections.Generic;

namespace TiendaServicios.Api.Autor.Modelo
{
    public class AutorLibro
    {
        public int AutorLibroId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime? FechaNacimiento { get; set; }


        //Un autor puede tener muchos grados
        public ICollection<GradoAcademico> ListaGradosAcademico { get; set; }

        //Valor universal unico para cuando seguir o transmitir una Instancia
        public string AutorLibroGuid { get; set; }//Global unique identifier

     
    }
}
