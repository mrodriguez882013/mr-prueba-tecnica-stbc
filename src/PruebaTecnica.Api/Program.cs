using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Api.Data;
using PruebaTecnica.Api.Middlewares;
using PruebaTecnica.Api.Repositories;
using PruebaTecnica.Api.Repositories.Interfaces;
using PruebaTecnica.Api.Services;
using PruebaTecnica.Api.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// ---------------------------------------------------------------------
// 1. CONFIGURACIÓN DE SERVICIOS BÁSICOS Y CONTROLLERS
// Agrega soporte para los controladores de API y la documentación OpenAPI (Swagger/Scalar)
// ---------------------------------------------------------------------
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer(); // Necesario para Swagger
builder.Services.AddSwaggerGen();           // Registra el generador de Swagger


// ---------------------------------------------------------------------
// 2. CONFIGURACIÓN DE BASE DE DATOS (Entity Framework Core)
// Registra el DbContext y configura la conexión a SQL Server usando la cadena del appsettings.json
// ---------------------------------------------------------------------
builder.Services.AddDbContext<PruebaTecnicaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PruebaTecnica"))
);


// ---------------------------------------------------------------------
// 3. INYECCIÓN DE DEPENDENCIAS - CAPA DE REPOSITORIOS (Acceso a Datos)
// Asocia las interfaces de repositorios con sus respectivas implementaciones concretas
// ---------------------------------------------------------------------
builder.Services.AddScoped<IProductoRepository, ProductoRepository>();
builder.Services.AddScoped<IVentaRepository, VentaRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();


// ---------------------------------------------------------------------
// 4. INYECCIÓN DE DEPENDENCIAS - CAPA DE SERVICIOS (Lógica de Negocio)
// Asocia las interfaces de servicios con sus implementaciones para la lógica y reglas de negocio
// ---------------------------------------------------------------------
builder.Services.AddScoped<IProductoService, ProductoService>();
builder.Services.AddScoped<IVentaService, VentaService>();
builder.Services.AddScoped<IAuthService, AuthService>();


var app = builder.Build();

// ---------------------------------------------------------------------
// 5. CONFIGURACIÓN DEL PIPELINE DE PETICIONES HTTP (MIDDLEWARES)
// Habilita la interfaz de OpenAPI únicamente en entornos de desarrollo
// ---------------------------------------------------------------------

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "PruebaTecnica Stbc API V1");
    });
}

// Middleware personalizado para interceptar y gestionar errores de forma global en toda la API
app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

// Nota: app.UseAuthorization() debe ir antes de app.MapControllers() si se usa seguridad por tokens/roles
app.UseAuthorization();

// Mapea las rutas hacia los Controllers de la API
app.MapControllers();

app.Run();