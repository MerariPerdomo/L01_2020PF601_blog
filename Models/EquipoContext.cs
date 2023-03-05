
using Microsoft.EntityFrameworkCore;
namespace L01_2020PF601.Models
{ 

    public class EquipoContext : DbContext
    {
        public EquipoContext(DbContextOptions<EquipoContext> options): base(options) {
    }

    public DbSet<usuarios> usuarios { get; set; }

    public DbSet<calificaciones> calificaciones { get;set; }
     
        public DbSet<comentarios> comentarios { get; set; }

        public DbSet<roles> roles { get; set; }
    }
}
