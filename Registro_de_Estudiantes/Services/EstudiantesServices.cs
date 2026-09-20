using Microsoft.EntityFrameworkCore;
using Registro_de_Estudiantes.DAL;
using Registro_de_Estudiantes.Models;
using System.Linq.Expressions;

namespace Registro_de_Estudiantes.Services;

public class EstudiantesServices(IDbContextFactory<Contexto> DbFactory)
{
    public async Task<bool> Guardar(Estudiantes estudiante)
    {
        if (estudiante.EstudianteId == 0)
        {
            return await Insertar(estudiante);
        }
        else
        {
            return await Modificar(estudiante);
        }
    }

    public async Task<bool> Insertar(Estudiantes estudiante)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Estudiantes.Add(estudiante);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Modificar(Estudiantes estudiante)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Update(estudiante);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<Estudiantes?> Buscar(int estudianteId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Estudiantes.AsNoTracking().FirstOrDefaultAsync(e => e.EstudianteId == estudianteId);
    }

    public async Task<bool> Existe(int estudianteId, string nombres)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Estudiantes
            .AnyAsync(e => e.EstudianteId != estudianteId && e.Nombres == nombres);
    }

    public async Task<bool> Eliminar(int estudianteId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Estudiantes
            .AsNoTracking()
            .Where(e => e.EstudianteId == estudianteId)
            .ExecuteDeleteAsync() > 0;
    }

    public async Task<List<Estudiantes>> Listar(Expression<Func<Estudiantes, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Estudiantes
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }
}
