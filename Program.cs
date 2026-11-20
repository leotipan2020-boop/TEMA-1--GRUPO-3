
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
builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddRoles<IdentityRole>()
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

// Crear roles y usuarios de prueba
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider
        .GetRequiredService<RoleManager<IdentityRole>>();

    var userManager = scope.ServiceProvider
        .GetRequiredService<UserManager<ApplicationUser>>();

    // Crear roles
    string[] roles = { "Administrador", "Cliente" };

    foreach (var rol in roles)
    {
        if (!await roleManager.RoleExistsAsync(rol))
        {
            var resultado = await roleManager.CreateAsync(
                new IdentityRole(rol));

            if (!resultado.Succeeded)
            {
                throw new Exception(
                    $"No se pudo crear el rol {rol}");
            }
        }
    }

    // Crear usuarios de prueba
    var usuarios = new[]
    {
        new { Correo = "admin@tienda.com", Rol = "Administrador" },
        new { Correo = "cliente@tienda.com", Rol = "Cliente" }
    };

    foreach (var usuario in usuarios)
    {
        var existente = await userManager.FindByEmailAsync(usuario.Correo);

        if (existente == null)
        {
            existente = new ApplicationUser
            {
                UserName = usuario.Correo,
                Email = usuario.Correo,
                EmailConfirmed = true
            };

            var resultado = await userManager.CreateAsync(
                existente, "Tienda2026!");

            if (!resultado.Succeeded)
            {
                throw new Exception(
                    string.Join(", ",
                        resultado.Errors.Select(e => e.Description)));
            }
        }

        // Asignar el rol correspondiente
        if (!await userManager.IsInRoleAsync(existente, usuario.Rol))
        {
            var resultadoRol = await userManager.AddToRoleAsync(
                existente, usuario.Rol);

            if (!resultadoRol.Succeeded)
            {
                throw new Exception(
                    string.Join(", ",
                        resultadoRol.Errors.Select(e => e.Description)));
            }
        }
    }
}

app.Run();
