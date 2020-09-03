using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GestionNonconformite_NCARouiba.Models.Entities
{
    public class DemandeEntretien
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public User Conducteur { get; set; }
        public int CarId { get; set; }
        public Car car { get; set; }
        [Required(ErrorMessage = "Champs obligatoire !")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy H:mm}")]
        public DateTime Date { get; set; }
        public string Urgence { get; set; }
        public string Commentaire { get; set; }

    }
}
