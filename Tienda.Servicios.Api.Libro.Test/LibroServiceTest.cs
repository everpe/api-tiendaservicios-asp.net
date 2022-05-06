using AutoMapper;
using GenFu;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaServicios.Api.Libro.Aplicacion;
using TiendaServicios.Api.Libro.Modelo;
using TiendaServicios.Api.Libro.Persistencia;
using Xunit;
namespace TiendaServicios.Api.Libro.Test
{



    public class LibroServiceTest
    {
        //Generar Data Falsa con Genfu
        private IEnumerable<LibreriaMaterial> ObtenerDataPrueba()
        {
            A.Configure<LibreriaMaterial>()
                .Fill(x => x.Titulo).AsArticleTitle()//Titulo random
                .Fill(x => x.LibreriaMaterialId, () => { return Guid.NewGuid(); });//id random
            //Crea lista de Libros
            var lista = A.ListOf<LibreriaMaterial>(30);
            lista[0].LibreriaMaterialId = Guid.Empty;
            return lista;
        }    


        private Mock<ContextoLibreria> CrearContextoBd()
        {
            var dataPrueba = ObtenerDataPrueba().AsQueryable();
            //CLASES REQUERIDAS DE ENTITY FRAMEWORK: Para Instanciar Contexto
            var dbSet = new Mock<DbSet<LibreriaMaterial>>();
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.Provider).Returns(dataPrueba.Provider);
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.Expression).Returns(dataPrueba.Expression);
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.ElementType).Returns(dataPrueba.ElementType);
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.GetEnumerator()).Returns(dataPrueba.GetEnumerator());

            dbSet.As<IAsyncEnumerable<LibreriaMaterial>>().Setup(x => x.GetAsyncEnumerator(new System.Threading.CancellationToken()))
                .Returns(new AsyncEnumerator<LibreriaMaterial>(dataPrueba.GetEnumerator()));


            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.Provider).Returns(new AsyncQueryProvider<LibreriaMaterial>(dataPrueba.Provider));


            //CREANDO EL CONTEXTOBD
            var contexto = new Mock<ContextoLibreria>();
            contexto.Setup(x => x.LibreriaMaterial).Returns(dbSet.Object);
            return contexto;
        }


        //Obtinee el libro Por id(Ejecutar el metodo...)
        [Fact]
        public async void GetBookByID()
        {
            var mockContexto = CrearContextoBd();
            var mapConfig = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingTest()); });
            
            var mapper = mapConfig.CreateMapper();

            var reqLirobUnico = new ConsultaFiltro.LibroUnico();
            reqLirobUnico.LibroId = Guid.Empty;//Busca el que se guardó vacio a propósito de primero

            var manejador = new ConsultaFiltro.Manejador(mockContexto.Object, mapper);
            var libro = await manejador.Handle(reqLirobUnico, new System.Threading.CancellationToken());
        
            Assert.NotNull(libro);
            //Tiene que devolver true buscando el id vacio
            Assert.True(libro.LibreriaMaterialId == Guid.Empty);
        }


        //Probar GetLibros de microservice Libros.
        [Fact]
        public async void GetLibros()
        {
            System.Diagnostics.Debugger.Launch();
            //1.Emular instancias inyectadas que se requieren en Consulta de libros.
            var mockContexto = CrearContextoBd();

            //2.Emular Mapping
            var configMap = new MapperConfiguration(cfg => cfg.AddProfile(new MappingTest()));
            var mapper = configMap.CreateMapper();
             
            //3.Instanciar Clase Manejador de Consulta de Libros.
            Consulta.Manejador manejador = new Consulta.Manejador(mockContexto.Object, mapper);
            Consulta.Ejecuta requestEjecuta = new Consulta.Ejecuta();

            var listaLibros = await manejador.Handle(requestEjecuta, new System.Threading.CancellationToken());
            Assert.True(listaLibros.Any());//Verifica la lista y posibles errrores
        }

        [Fact]
        public async void saveLibro()
        {
            //System.Diagnostics.Debugger.Launch ();
            //Crear Bd en Memoria
            var options = new DbContextOptionsBuilder<ContextoLibreria>()
                .UseInMemoryDatabase(databaseName: "BaseDatosLibro")
                .Options;
            var contexto = new ContextoLibreria(options);

            var request = new Nuevo.Ejecuta();
            request.Titulo = "Libro de microservicios";
            request.AutorLibro = Guid.Empty;
            request.FechaPublicacion = DateTime.Now;

            //Instancia clase que inserta
            var manejador = new Nuevo.Manejador(contexto);

            var libroInsert = await manejador.Handle(request, new System.Threading.CancellationToken());
            Assert.True(libroInsert!=null);
            
        }

    }
}
