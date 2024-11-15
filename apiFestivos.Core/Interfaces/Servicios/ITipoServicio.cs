using apiFestivos.Dominio.Entidades;

namespace apiFestivos.Core.Interfaces.Servicios
{
    public interface ITipoServicio
    {
        Task<IEnumerable<Tipo>> ObtenerTodos();

        Task<Tipo> Obtener(int Id);

        Task<IEnumerable<Tipo>> Buscar(string Dato);

        Task<Tipo> Agregar(Tipo Tipo);

        Task<Tipo> Modificar(Tipo Tipo);

        Task<bool> Eliminar(int Id);
    }
}
