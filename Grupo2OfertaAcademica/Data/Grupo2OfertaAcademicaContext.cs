using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Grupo2OfertaAcademica.Models;

namespace Grupo2OfertaAcademica.Data
{
    public class Grupo2OfertaAcademicaContext
        : IdentityDbContext<ApplicationUser>
    {
        public Grupo2OfertaAcademicaContext(
            DbContextOptions<Grupo2OfertaAcademicaContext> options)
            : base(options)
        {
        }

        public DbSet<Carrera> Carrera { get; set; } = default!;

        public DbSet<Facultad> Facultad { get; set; } = default!;
    }
}