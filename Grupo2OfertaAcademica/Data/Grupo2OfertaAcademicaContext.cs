using Microsoft.EntityFrameworkCore;

public class Grupo2OfertaAcademicaContext(DbContextOptions<Grupo2OfertaAcademicaContext> options) : DbContext(options)
{
    public DbSet<Grupo2OfertaAcademica.Models.Carrera> Carrera { get; set; } = default!;
}
