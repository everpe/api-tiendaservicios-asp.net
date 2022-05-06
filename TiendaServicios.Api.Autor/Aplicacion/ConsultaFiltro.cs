using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaServicios.Api.Autor.Modelo;
using TiendaServicios.Api.Autor.Persistencia;

namespace TiendaServicios.Api.Autor.Aplicacion
{
    public class ConsultaFiltro {
        public class AutorUnico : IRequest<AutorDto> 
        {
            public string AutorGuid { get; set; }
        }


        public class Manejador : IRequestHandler<AutorUnico, AutorDto>
        {
            private readonly ContextoAutor _contexto;
            //Inject Mapper de DTO
            private readonly IMapper _mapper;
            public Manejador(ContextoAutor contextAutor, IMapper mapper)
            {
                _contexto = contextAutor;
                _mapper = mapper;
            }

            public async Task<AutorDto> Handle(AutorUnico request, CancellationToken cancellationToken)
            {
                var autor = await _contexto.AutorLibro.Where(aulib => aulib.AutorLibroGuid == request.AutorGuid).FirstOrDefaultAsync();
                if( autor == null ) {
                    throw new Exception("");
                }
                var autorDto = _mapper.Map<AutorLibro, AutorDto>(autor);
                return autorDto;
            }
        }
    }
}
