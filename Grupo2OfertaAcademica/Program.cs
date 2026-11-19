using Grupo2OfertaAcademica.Models;
using Grupo2OfertaAcademica.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;



namespace Grupo2OfertaAcademica
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("Grupo2OfertaAcademicaContext") ?? throw new InvalidOperationException("Connection string 'Grupo2OfertaAcademicaContext' not found.");

            builder.Services.AddDbContext<Grupo2OfertaAcademicaContext>( options => options.UseSqlServer(builder.Configuration.GetConnectionString("Grupo2OfertaAcademicaContext")));

            builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false).AddRoles<IdentityRole>() .AddEntityFrameworkStores<Grupo2OfertaAcademicaContext>();

            // Add services to the container.
            builder.Services.AddControllersWithViews(options =>
            {
                options.ModelBindingMessageProvider
                    .SetValueMustNotBeNullAccessor(
                        _ => "Este campo es obligatorio.");

                options.ModelBindingMessageProvider
                    .SetValueMustBeANumberAccessor(
                        campo => $"El campo {campo} debe ser un número.");

                options.ModelBindingMessageProvider
                    .SetAttemptedValueIsInvalidAccessor(
                        (valor, campo) => $"El valor '{valor}' no es válido para {campo}.");
            });

            var app = builder.Build();



            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider
                    .GetRequiredService<RoleManager<IdentityRole>>();

                var userManager = scope.ServiceProvider
                    .GetRequiredService<UserManager<ApplicationUser>>();

                // 1. Crear roles
                string[] roles = { "Coordinador", "Aspirante" };

                foreach (var rol in roles)
                {
                    if (!await roleManager.RoleExistsAsync(rol))
                    {
                        var resultado = await roleManager.CreateAsync(
                            new IdentityRole(rol));

                        if (!resultado.Succeeded)
                            throw new Exception(
                                string.Join("; ", resultado.Errors.Select(e => e.Description)));
                    }
                }

                // 2. Usuarios de prueba
                var usuarios = new[]
                {
        (Correo: "coordinador@grupo2.test",
         Clave: "Cordinador66*",
         Rol: "Coordinador"),

        (Correo: "aspirante@grupo2.test",
         Clave: "Aspirante66*",
         Rol: "Aspirante")
    };

                foreach (var datos in usuarios)
                {
                    var usuario = await userManager.FindByEmailAsync(datos.Correo);

                    // Crear solo si no existe
                    if (usuario == null)
                    {
                        usuario = new ApplicationUser
                        {
                            UserName = datos.Correo,
                            Email = datos.Correo
                        };

                        var resultado = await userManager.CreateAsync(
                            usuario, datos.Clave);

                        if (!resultado.Succeeded)
                            throw new Exception(
                                string.Join("; ", resultado.Errors.Select(e => e.Description)));
                    }

                    // Asignar rol, incluso si el usuario ya existía
                    if (!await userManager.IsInRoleAsync(usuario, datos.Rol))
                    {
                        var resultado = await userManager.AddToRoleAsync(
                            usuario, datos.Rol);

                        if (!resultado.Succeeded)
                            throw new Exception(
                                string.Join("; ", resultado.Errors.Select(e => e.Description)));
                    }
                }
            }



            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();
            app.MapRazorPages();
            app.Run();
        }
    }
}
