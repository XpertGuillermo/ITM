using apiFestivos.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace apiFestivos.Infraestructura.Repositorio.Contextos
{
    public class FestivosContexto : DbContext
    {
        public FestivosContexto(DbContextOptions<FestivosContexto> options)
           : base(options)
        {
        }

        public DbSet<Tipo> Tipos { get; set; }

        public DbSet<Festivo> Festivos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tipo>(entidad =>
            {
                entidad.HasKey(e => e.Id);
                entidad.HasIndex(e => e.Nombre).IsUnique();
            });

            modelBuilder.Entity<Festivo>(entidad =>
            {
                entidad.HasKey(e => e.Id);
                entidad.HasIndex(e => e.Nombre).IsUnique();
            });

            modelBuilder.Entity<Festivo>()
              .HasOne(f => f.Tipo)
              .WithMany()
              .HasForeignKey(f => f.IdTipo)
              .IsRequired(false);
        }
    }
}
