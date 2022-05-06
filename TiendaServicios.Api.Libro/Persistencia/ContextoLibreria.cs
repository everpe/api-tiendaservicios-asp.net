using Microsoft.EntityFrameworkCore;
using TiendaServicios.Api.Libro.Modelo;

namespace TiendaServicios.Api.Libro.Persistencia
{
    //Representa al Entity Framework(BD)
    public class ContextoLibreria : DbContext
    {
        public ContextoLibreria() { }
        //Para que la conexión arranque al momento q se ejecute clase Program.cs
        public ContextoLibreria(DbContextOptions<ContextoLibreria> options ) : base(options) { }

        //Declaro clases como Entities(TablasBD)
        public virtual DbSet<LibreriaMaterial> LibreriaMaterial { get; set; }
        //virtual para poder cambiarle valores en Tests
    }
}
