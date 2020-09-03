using GestionNonconformite_NCARouiba.Models.Entities;
using GestionNonconformite_NCARouiba.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GestionNonconformite_NCARouiba.ViewModels
{
    public class UserCreateViewModel
    {
        [Required(ErrorMessage = "Remplissez le champs : Nom")]
        public string Nom { get; set; }
        [Required(ErrorMessage = "Remplissez le champs : Prénom")]
        public string Prenom { get; set; }
        public IFormFile Photo { get; set; }
        [Required(ErrorMessage = "Remplissez le champs : Fonction")]
        public string Fonction { get; set; }
        public string Adresse { get; set; }
      
        [Required(ErrorMessage = "Remplissez le champs : Email")]
        [EmailAddress]
        [Remote(action: "IsEmailInUser", controller:"Account")]
        [ValidEmailDomainAttribute(allowedDomain: "esi.dz",ErrorMessage = "Le domaine doit etre esi.dz")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Remplissez le champs : Mot de passe")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Retapez le mot même passe")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Mot de passe de confirmation différent !")]
        public string PasswordConfirm { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        public List<SelectRolesViewModel> UserRoles { get; set; }
    }
}
