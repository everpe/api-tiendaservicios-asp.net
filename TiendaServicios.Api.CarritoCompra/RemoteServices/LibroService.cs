using System.Text.Json;
using TiendaServicios.Api.CarritoCompra.RemoteInterface;
using TiendaServicios.Api.CarritoCompra.RemoteModels;

namespace TiendaServicios.Api.CarritoCompra.RemoteServices
{
    //Implementación de la comunicación con microservice Libros
    public class LibroService : ILibroService
    {
        private readonly IHttpClientFactory _httpClient;
        //Objeto parar imprimir mensajes en pantalla.
        private readonly ILogger _logger;
        public LibroService(IHttpClientFactory httpClient, ILogger logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }
        public async Task<(bool resultado, LibroRemote libro, string ErrorMessage)> GetLibro(Guid LibroId)
        {
            try {
                //Servicio que se agregó en Program.cs
                var cliente = _httpClient.CreateClient("Libros");
                var response = await cliente.GetAsync($"api/Libro/GetBookById/{LibroId}");
                if (response.IsSuccessStatusCode) {
                    //Valor de libro response
                    var contenido = await response.Content.ReadAsStringAsync(); 
                    //Se convierte a formato Json ignorando Mayusculas o minuscular en sus propiedades
                    var options = new JsonSerializerOptions() {  PropertyNameCaseInsensitive = true };
                    //Resultado en Modelo de clase LibroRemote
                    var resultado = JsonSerializer.Deserialize<LibroRemote>(contenido, options);
                    return (true, resultado, null);
                }
                return(false, null, response.ReasonPhrase);
            }
            catch(Exception e) {
                _logger?.LogError(e.ToString());
                return (false, null, e.Message);
            }
        }
    }
}
