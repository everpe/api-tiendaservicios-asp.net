using Microsoft.EntityFrameworkCore;
using TiendaServicios.Api.Autor.Modelo;

namespace TiendaServicios.Api.Autor.Persistencia
{
                //Representa al Entity Framework(BD)
    public class ContextoAutor : DbContext 
    {
        //Para que la conexión arranque al momento q se ejecute clase StartUp.cs
        public ContextoAutor(DbContextOptions<ContextoAutor> options) : base(options) { }

        //Declaro clases como Entities(TablasBD)
        public DbSet<AutorLibro> AutorLibro { get; set; }
        public DbSet<GradoAcademico> GradoAcademico { get; set; }
    }
}
