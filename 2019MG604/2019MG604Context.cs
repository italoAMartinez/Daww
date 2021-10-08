using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using _2019MG604.Models;
namespace _2019MG604
{
    public class _2019MG604Context : DbContext
    {
        public _2019MG604Context(DbContextOptions<_2019MG604Context> options) : base(options)
        {
            
        }
        public DbSet<equipos> equipos {get; set;}
        public DbSet<estados_equipo> estados_equipo {get; set;}
        public DbSet<marcas> marcas {get; set;}
        public DbSet<tipo_equipo> tipo_equipo {get; set;}
    }
}