using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaServicios.Api.Libro.Modelo;
using TiendaServicios.Api.Libro.Persistencia;

namespace TiendaServicios.Api.Libro.Aplicacion
{
    public class ConsultaFiltro
    {

        public class LibroUnico : IRequest<LibreriaMaterialDto> { 
            public Guid?  LibroId { get; set; }//Parámetro de la búsqueda ById
        }
        public class Manejador : IRequestHandler<LibroUnico,LibreriaMaterialDto>
        {
            private readonly ContextoLibreria  _context;
            private readonly IMapper _mapper;
            public Manejador(ContextoLibreria contextoBD, IMapper mapper)
            {
                _context = contextoBD;
                _mapper = mapper;
            }
            public async Task<LibreriaMaterialDto> Handle(LibroUnico request, CancellationToken cancellationToken)
            {
                Console.WriteLine("Request llega: Hola holaaaa");
                Console.WriteLine("Request llega::::::  "+ request);
                var libro = await _context.LibreriaMaterial.Where(lib => lib.LibreriaMaterialId == request.LibroId).FirstOrDefaultAsync();
                if (libro == null)
                {
                    throw new Exception("");
                }
                var libreriaDto = _mapper.Map<LibreriaMaterial, LibreriaMaterialDto>(libro);
                return libreriaDto;
            }
        }
    }
}
