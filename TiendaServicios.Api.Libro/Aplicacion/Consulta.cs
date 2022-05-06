using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaServicios.Api.Libro.Modelo;
using TiendaServicios.Api.Libro.Persistencia;

namespace TiendaServicios.Api.Libro.Aplicacion
{
    public class Consulta
    {

        //Patrón CQRS
        public class Ejecuta : IRequest<List<LibreriaMaterialDto>> { }

        public class Manejador : IRequestHandler<Ejecuta, List<LibreriaMaterialDto>>
        {
            private readonly ContextoLibreria _contexto;
            private readonly IMapper _mapper;

            public Manejador(ContextoLibreria contexto, IMapper mapper) {
                _contexto = contexto;
                _mapper = mapper;
            }

            public async Task<List<LibreriaMaterialDto>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var libros = await _contexto.LibreriaMaterial.ToListAsync();
                //Mapeo DTO
                var librosDto = _mapper.Map<List<LibreriaMaterial>, List<LibreriaMaterialDto> >(libros);
                return librosDto;
            }
        }
    }
}
