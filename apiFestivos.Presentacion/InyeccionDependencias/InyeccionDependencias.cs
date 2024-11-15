using apiFestivos.Aplicacion.Servicios;
using apiFestivos.Core.Interfaces.Repositorios;
using apiFestivos.Core.Interfaces.Servicios;
using apiFestivos.Infraestructura.Repositorio.Contextos;
using apiFestivos.Infraestructura.Repositorio.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace apiFestivos.Presentacion.InyeccionDependencias
{
    public static class InyeccionDependencias
    {

        public static IServiceCollection AgregarDependencias(this IServiceCollection servicios, IConfiguration configuracion)
        {
            //Agregar el DBContext
            servicios.AddDbContext<FestivosContexto>(opcionesConstruccion =>
            {
                opcionesConstruccion.UseSqlServer(configuracion.GetConnectionString("Festivos"));
            });

            //Agregar los repositorios
            servicios.AddTransient<ITipoRepositorio, TipoRepositorio>();
            servicios.AddTransient<IFestivoRepositorio, FestivoRepositorio>();


            //Agregar los servicios
            servicios.AddTransient<ITipoServicio, TipoServicio>();
            servicios.AddTransient<IFestivoServicio, FestivoServicio>();

            servicios.AddSingleton<IConfiguration>(configuracion);

            return servicios;
        }
    }

}
