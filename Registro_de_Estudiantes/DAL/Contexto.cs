using Microsoft.EntityFrameworkCore;
using Registro_de_Estudiantes.Models;

namespace Registro_de_Estudiantes.DAL;

public class Contexto : DbContext
{
    public DbSet<Estudiantes> Estudiantes { get; set; }

    public Contexto(DbContextOptions<Contexto> options) : base(options)
    {
    }
}
