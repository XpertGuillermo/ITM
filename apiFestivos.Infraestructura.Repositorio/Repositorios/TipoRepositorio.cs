using apiFestivos.Core.Interfaces.Repositorios;
using apiFestivos.Dominio.Entidades;
using apiFestivos.Infraestructura.Repositorio.Contextos;
using Microsoft.EntityFrameworkCore;

namespace apiFestivos.Infraestructura.Repositorio.Repositorios
{
       public class TipoRepositorio : ITipoRepositorio
    {
        private FestivosContexto context;

        public TipoRepositorio(FestivosContexto context)
        {
            this.context = context;
        }

        public async Task<Tipo> Obtener(int Id)
        {
            return (Tipo)(await context.Tipos
                .FirstOrDefaultAsync(item => item.Id == Id));
        }

        public async Task<IEnumerable<Tipo>> ObtenerTodos()
        {
            return await context.Tipos
                .ToListAsync();
        }

        public async Task<IEnumerable<Tipo>> Buscar(string Dato)
        {
            return await context.Tipos
                                   .Where(item => item.Nombre.Contains(Dato)) // Filtrar elementos 
                                   .ToListAsync(); // Convertir a una lista IEnumerable<Tipo>
        }

        public async Task<Tipo> Agregar(Tipo Tipo)
        {
            context.Tipos.Add(Tipo);
            await context.SaveChangesAsync();
            return Tipo;
        }
        public async Task<Tipo> Modificar(Tipo Tipo)
        {
            var TipoExistente = await context.Tipos.FindAsync(Tipo.Id);
            if (TipoExistente == null)
            {
                return null;
            }
            context.Entry(TipoExistente).CurrentValues.SetValues(Tipo);
            await context.SaveChangesAsync();
            return (Tipo)(await context.Tipos.FindAsync(Tipo.Id));
        }

        public async Task<bool> Eliminar(int Id)
        {
            var TipoExistente = await context.Tipos.FindAsync(Id);
            if (TipoExistente == null)
            {
                return false;
            }

            context.Tipos.Remove(TipoExistente);
            await context.SaveChangesAsync();
            return true;
        }

    }

}
