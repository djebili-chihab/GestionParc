
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GestionNonconformite_NCARouiba.Models.Entities
{
    public class Car
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public User Conducteur { get; set; }
        public string Matricule { get; set; }
        [Required(ErrorMessage = "Champs obligatoire !")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateAssurance { get; set; }
        [Required(ErrorMessage = "Champs obligatoire !")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateControlTech { get; set; }
        [Required(ErrorMessage = "Champs obligatoire !")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateScannaire { get; set; }
        public string marque { get; set; }
        public string Categorie { get; set; }
        public string Kilometrage { get; set; }
        public string Carburant { get; set; }
        public string Photo { get; set; }
        public string Statue { get; set; }
        public string EnMission { get; set; }
        public string NumChassis { get; set; }
        public string Couleur { get; set; }
        public string model { get; set; }
        
    }
}
