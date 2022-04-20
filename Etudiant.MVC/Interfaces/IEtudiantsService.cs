
using Etudiants.MVC.Models;

namespace Etudiants.MVC.Interfaces
{
    public interface IEtudiantsService
    {
        Task<IEnumerable<Etudiant>> ObtenirTout();

        Task Ajouter(Etudiant etudiant);
    }
}
