using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GestionNonconformite_NCARouiba.ViewModels
{
    public class LogingViewModel
    {
        [Required(ErrorMessage = "Remplissez le champs : Email")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Remplissez le champs : Mot de passe")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name=" Restez connecté")]
        public Boolean RemenberMe { get; set; }
    }
}
