using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.Autor.Modelo;
using TiendaServicios.Api.Autor.Persistencia;

namespace TiendaServicios.Api.Autor.Aplicacion
{
    public class Nuevo
    {
        //Clase intermedia que recibe Parametros del Controller.
        public class Ejecuta: IRequest {
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public DateTime? FechaNacimiento { get; set; }
        }

        //FluentValidation Insert Autor()
        public class EjecutaValidation : AbstractValidator<Ejecuta> {
            public EjecutaValidation() {
                RuleFor(x => x.Nombre).NotEmpty();
                RuleFor(x => x.Apellido).NotEmpty();
            }
        }




        //Realiza la logica de inserción a la BD
        public class Manejador : IRequestHandler<Ejecuta>
        {
            public readonly ContextoAutor _contexto;
            public Manejador(ContextoAutor contextAutor)
            {
                _contexto = contextAutor;
            }

            //1=> registro exitoso, 0=> Errores en registro
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var autorLibro = new AutorLibro
                {
                    Nombre = request.Nombre,
                    Apellido = request.Apellido,
                    FechaNacimiento = request.FechaNacimiento,
                    AutorLibroGuid = Convert.ToString(Guid.NewGuid()),
                };
                _contexto.AutorLibro.Add(autorLibro);
                var valor = await _contexto.SaveChangesAsync();//Realiza transacción y retorna resultado.
                if(valor > 0) {
                    return Unit.Value;
                }
                throw new Exception("No se pudó insertar el autor");
            }
        }
    }
}
