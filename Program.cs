
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TiendaOnline.Data;

var builder = WebApplication.CreateBuilder(args);

// Conexion con SQL Server
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException(
        "No se encontró la cadena de conexión 'DefaultConnection'.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Configuracion de Identity
builder.Services.AddDefaultIdentity<TiendaOnline.Data.ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>();

// Servicios MVC y Razor Pages
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configuracion de la aplicacion
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

// Autenticacion y autorizacion
app.UseAuthentication();
app.UseAuthorization();

// Archivos estaticos
app.MapStaticAssets();

// Rutas de los controladores
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

// Paginas de Identity
app.MapRazorPages();

app.Run();
