using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionNonconformite_NCARouiba.Models.Entities
{
    public class User : IdentityUser
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string PhotoPath { get; set; }
        public string Fonction { get; set; }
        public string Adresse { get; set; }
        public string Password { get; set; }
    }
}
