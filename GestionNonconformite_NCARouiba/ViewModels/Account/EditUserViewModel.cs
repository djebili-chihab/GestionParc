using GestionNonconformite_NCARouiba.Utilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GestionNonconformite_NCARouiba.ViewModels
{
    public class EditUserViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Remplissez le champs : Nom")]
        public string Nom { get; set; }
        [Required(ErrorMessage = "Remplissez le champs : Prénom")]
        public string Prenom { get; set; }
        public Microsoft.AspNetCore.Http.IFormFile Photo { get; set; }
        [Required(ErrorMessage = "Remplissez le champs : Fonction")]
        public string Fonction { get; set; }
        public string Adresse { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Remplissez le champs : Email")]
        [ValidEmailDomainAttribute(allowedDomain: "esi.dz", ErrorMessage = "Le domaine doit etre esi.dz")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Mot de passe de confirmation différent !")]
        public string PasswordConfirm { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        public List<SelectRolesViewModel> UserRoles { get; set; }
    }
}
