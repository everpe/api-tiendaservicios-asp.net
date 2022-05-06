using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaServicios.Api.Libro.Aplicacion;
using TiendaServicios.Api.Libro.Persistencia;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Add fluentValidation Only Once Time in All Project,Agrega validador a Clase Nuevo
builder.Services.AddControllers().AddFluentValidation(
    cfg => cfg.RegisterValidatorsFromAssemblyContaining<Nuevo>()
);


//Agregar contexto BD
builder.Services.AddDbContext<ContextoLibreria>( opt => {
    opt.UseSqlServer(builder.Configuration.GetConnectionString("ConexionDB"));
});
//Inyecta clase Manejador a mediaTr (Mediator)
builder.Services.AddMediatR(typeof(Nuevo.Manejador).Assembly);
//Inyecta AutoMapper en todo el Proyecto
builder.Services.AddAutoMapper(typeof(Consulta.Ejecuta).Assembly);



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
