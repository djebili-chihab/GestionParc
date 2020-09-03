using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GestionNonconformite_NCARouiba.Models.Entities
{
    public class DemandeCarburant
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public User Conducteur { get; set; }
        public int CarId { get; set; }
        public Car car { get; set; }
        [Required(ErrorMessage = "Champs obligatoire !")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy }")]
        public DateTime Date { get; set; }
        public long  NouveauKM { get; set; }
        public string PreuveKM { get; set; }
        public string Commentaire { get; set; }
        
    }
}
