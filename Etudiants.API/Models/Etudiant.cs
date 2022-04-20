using System.ComponentModel.DataAnnotations;

namespace Etudiants.API.Models
{
    public class Etudiant
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Le nom est obligatoire")]
        public string Nom { get; set; }

        [Required(ErrorMessage = "Le prénom est obligatoire")]
        public string Prenom { get; set; }

        [Required(ErrorMessage = "Le NAS est obligatoire")]
        public string NAS { get; set; }

        [Required(ErrorMessage = "La date d'inscription est obligatoire")]
        public DateTime DateInscription { get; set; }
    }
}
