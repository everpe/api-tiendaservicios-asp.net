using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaServicios.Api.Autor.Aplicacion;
using TiendaServicios.Api.Autor.Persistencia;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
// Add fluentValidation Only Once Time in All Project
builder.Services.AddControllers().AddFluentValidation(
    cfg => cfg.RegisterValidatorsFromAssemblyContaining<Nuevo>()
);//Agrega validador a Clase Nuevo

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Agregando Context de BD
builder.Services.AddDbContext<ContextoAutor>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("ConexionDatabase"));
});

//Inyecta clase Manejador a mediaTr (Mediator)
builder.Services.AddMediatR(typeof(Nuevo.Manejador).Assembly);
//Instancia Todos los Mapper DTO del Proyecto solo usar una clase de referencia
builder.Services.AddAutoMapper(typeof(Consulta.Manejador));
var app = builder.Build();




//Para error de zonaHoraria
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
