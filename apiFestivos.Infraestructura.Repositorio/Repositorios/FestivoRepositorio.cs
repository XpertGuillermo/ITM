
using apiFestivos.Dominio.Entidades;
using apiFestivos.Infraestructura.Repositorio.Contextos;
using Microsoft.EntityFrameworkCore;
using apiFestivos.Core.Interfaces.Repositorios;

namespace apiFestivos.Infraestructura.Repositorio.Repositorios
{
        public class FestivoRepositorio : IFestivoRepositorio
    {
        private FestivosContexto context;

        public FestivoRepositorio(FestivosContexto context)
        {
            this.context = context;
        }

        public async Task<Festivo> Obtener(int Id)
        {
            return (Festivo)(await context.Festivos
                .Include(item => item.Tipo) // Incluye el Tipo
                .FirstOrDefaultAsync(item => item.Id == Id));
        }

        public async Task<IEnumerable<Festivo>> ObtenerTodos()
        {
            return await context.Festivos
                .Include(item => item.Tipo) // Incluye el Tipo
                .ToListAsync();
        }

        public async Task<IEnumerable<Festivo>> Buscar(string Dato)
        {
            return await context.Festivos
                                   .Where(item => item.Nombre.Contains(Dato)) // Filtrar elementos 
                                   .Include(item => item.Tipo) // Incluye el Tipo
                                   .ToListAsync(); // Convertir a una lista IEnumerable<Festivo>
        }

        public async Task<Festivo> Agregar(Festivo Festivo)
        {
            context.Festivos.Add(Festivo);
            await context.SaveChangesAsync();
            return Festivo;
        }
        public async Task<Festivo> Modificar(Festivo Festivo)
        {
            var FestivoExistente = await context.Festivos.FindAsync(Festivo.Id);
            if (FestivoExistente == null)
            {
                return null;
            }
            context.Entry(FestivoExistente).CurrentValues.SetValues(Festivo);
            await context.SaveChangesAsync();
            return (Festivo)(await context.Festivos.FindAsync(Festivo.Id));
        }

        public async Task<bool> Eliminar(int Id)
        {
            var FestivoExistente = await context.Festivos.FindAsync(Id);
            if (FestivoExistente == null)
            {
                return false;
            }

            context.Festivos.Remove(FestivoExistente);
            await context.SaveChangesAsync();
            return true;
        }

    }

}
