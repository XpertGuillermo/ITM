using apiFestivos.Dominio.Entidades;

namespace apiFestivos.Core.Interfaces.Repositorios
{
    public interface ITipoRepositorio
    {
        Task<IEnumerable<Tipo>> ObtenerTodos();

        Task<Tipo> Obtener(int Id);

        Task<IEnumerable<Tipo>> Buscar(string Dato);

        Task<Tipo> Agregar(Tipo Tipo);

        Task<Tipo> Modificar(Tipo Tipo);

        Task<bool> Eliminar(int Id);
    }
}
