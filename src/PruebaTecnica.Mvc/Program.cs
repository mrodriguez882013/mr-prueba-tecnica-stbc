var builder = WebApplication.CreateBuilder(args);

// ---------------------------------------------------------------------
// 1. CONFIGURACIÓN DE SERVICIOS BÁSICOS Y CONTROLLERS
// ---------------------------------------------------------------------
builder.Services.AddControllersWithViews();

// ---------------------------------------------------------------------
// 2. CONFIGURACIÓN DEL CLIENTE HTTP PARA CONSUMIR LA API REST
// ---------------------------------------------------------------------
builder.Services.AddHttpClient("ApiClient", client =>
{
    // Ajusta el puerto exacto en el que corre tu Web API
    client.BaseAddress = new Uri("https://localhost:7094/");
});

// ---------------------------------------------------------------------
// 3. CONFIGURACIÓN DE SESIONES PARA CONTROL DE ROLES Y USUARIO
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
// ---------------------------------------------------------------------
if (!app.Environment.IsDevelopment())
{
    // Redirige a la vista de error amigable en producción
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    // Muestra errores detallados únicamente en entorno de desarrollo local
    app.UseDeveloperExceptionPage();
}

// Redirige códigos de estado HTTP (como 404 o 400 no controlados) al manejador de errores
app.UseStatusCodePagesWithReExecute("/Home/Error", "?statusCode={0}");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// ---------------------------------------------------------------------
// 5. ACTIVACIÓN DE SESIÓN Y SEGURIDAD
// ¡OBLIGATORIO! UseSession debe ir estrictamente antes de UseAuthorization
// ---------------------------------------------------------------------
app.UseSession();

app.UseAuthorization();

app.MapStaticAssets();

// ---------------------------------------------------------------------
// 6. CONFIGURACIÓN DE RUTAS POR DEFECTO
// Inicia directamente en el módulo de Autenticación (Login)
// ---------------------------------------------------------------------
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}")
    .WithStaticAssets();

app.Run();