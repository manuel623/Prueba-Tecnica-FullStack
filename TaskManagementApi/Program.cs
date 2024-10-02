using Microsoft.EntityFrameworkCore;
using TaskManagementApi.Models;
using TaskManagementApi.Service;

var builder = WebApplication.CreateBuilder(args);

// configuracion de servicios || service configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TaskManagementContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions => sqlOptions.EnableRetryOnFailure()
    )
);

// se habilitan CORS || CORS is enabled
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

builder.Services.AddControllers();

// registra servicio de state  || register state service
builder.Services.AddScoped<IStateService, StateService>();

// se construye la aplicacion
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseRouting();

// se habilitan todos CORS || CORS is enabled
app.UseCors("AllowAll");
// se asegura que el middleware de API Key este registrado aqui || make sure the API Key middleware is registered here
app.UseMiddleware<ApiKeyMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();

// se mapean los controladores || controllers are mapped
app.MapControllers();

// se ejecuta la aplicación || the application is executed
app.Run();
