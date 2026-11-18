using Microsoft.EntityFrameworkCore;
namespace Grupo2OfertaAcademica
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("Grupo2OfertaAcademicaContext") ?? throw new InvalidOperationException("Connection string 'Grupo2OfertaAcademicaContext' not found.");

            builder.Services.AddDbContext<Grupo2OfertaAcademicaContext>(options => options.UseSqlServer(connectionString));

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

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
