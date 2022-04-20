using Etudiants.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Etudiants.API.Data
{
    public class EtudiantsContext : DbContext
    {
        public EtudiantsContext(DbContextOptions<EtudiantsContext> options)
          : base(options)
        {
        }

        public DbSet<Etudiant> Etudiants { get; set; }
    }
}
