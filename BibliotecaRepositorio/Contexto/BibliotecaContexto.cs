using BibliotecaRepositorio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaRepositorio.Contexto
{
    public class BibliotecaContexto : DbContext
    {
        public BibliotecaContexto(DbContextOptions<BibliotecaContexto> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<LibroEntidad> Libros { get; set; }
        public DbSet<PrestamoEntidad> Prestamos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseInMemoryDatabase("BibliotecaBD");
            }
        }
    }
}