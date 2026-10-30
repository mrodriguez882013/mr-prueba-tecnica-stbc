var builder = WebApplication.CreateBuilder(args);

// ---------------------------------------------------------------------
// 1. CONFIGURACIÓN DE SERVICIOS BÁSICOS Y CONTROLLERS
// Agrega soporte para los controladores y vistas del patrón MVC
// ---------------------------------------------------------------------
builder.Services.AddControllersWithViews();


// ---------------------------------------------------------------------
// 2. CONFIGURACIÓN DEL CLIENTE HTTP PARA CONSUMIR LA API REST
// Registra HttpClient con la URL base de tu API para inyectarlo en los controladores
// ---------------------------------------------------------------------
builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7094/"); // Ajusta el puerto según tu API
});


// ---------------------------------------------------------------------
// 3. CONFIGURACIÓN DE SESIONES PARA CONTROL DE ROLES Y USUARIO
// Habilita el almacenamiento de sesión para persistir el login, rol y usuario actual
// ---------------------------------------------------------------------
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


var app = builder.Build();

// ---------------------------------------------------------------------
// 4. CONFIGURACIÓN DEL PIPELINE DE PETICIONES HTTP (MIDDLEWARES)
// Configuración de manejo de excepciones para entornos de producción
// ---------------------------------------------------------------------
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// ---------------------------------------------------------------------
// 5. ACTIVACIÓN DE SESIÓN Y SEGURIDAD
// El middleware de sesión debe ir obligatoriamente antes de UseAuthorization y MapControllerRoute
// ---------------------------------------------------------------------
app.UseSession();

app.UseAuthorization();

app.MapStaticAssets();

// ---------------------------------------------------------------------
// 6. CONFIGURACIÓN DE RUTAS POR DEFECTO
// Redirige la ruta inicial directamente al controlador de Autenticación (Login)
// ---------------------------------------------------------------------
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}")
    .WithStaticAssets();

app.Run();