using Microsoft.EntityFrameworkCore;
using TiendaServicios.Api.CarritoCompra.Modelo;

namespace TiendaServicios.Api.CarritoCompra.Persistencia
{
    public class ContextoBd : DbContext
    {
        public ContextoBd(DbContextOptions<ContextoBd> options) : base(options) { }

        public DbSet<CarritoSesion> CarritoSesionTable { get; set; }
        public DbSet<CarritoSesionDetalle> CarritoSesiondetalleTable { get; set; }

    }
    
}
