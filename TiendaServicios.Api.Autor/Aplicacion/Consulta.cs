using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaServicios.Api.Autor.Modelo;
using TiendaServicios.Api.Autor.Persistencia;

namespace TiendaServicios.Api.Autor.Aplicacion
{
    public class Consulta {

        //Clase para Modelar el resultado de la consulta a BD
        public class ListaAutor : IRequest<List<AutorDto>> { }




        //El manejador recibe request del controler, y el retorno  de la consulta a BD.
        public class Manejador : IRequestHandler<ListaAutor, List<AutorDto>> {
            private readonly ContextoAutor _contexto;
            //Mapper de DTO
            private readonly IMapper _mapper;

            public Manejador(ContextoAutor contextAutor, IMapper mapper ) {
                _contexto = contextAutor;
                _mapper = mapper;
            }


            //Impmenetación MediatR para manejo de BD
            public async Task<List<AutorDto>> Handle(ListaAutor request, CancellationToken cancellationToken) {
                var autores = await _contexto.AutorLibro.ToListAsync();
                //Mapper para DTO
                //Recibe lista de AutorLibro y retorna Lista de AutorDto
                var autoresDto = _mapper.Map<List<AutorLibro>, List<AutorDto>>(autores);
                return autoresDto;
            }
        }
    }
}
