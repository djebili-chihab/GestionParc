using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionNonconformite_NCARouiba.ViewModels
{
    public class UserRoleViewModel
    {
        public string Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Fonction { get; set; }
        public Boolean Selected { get; set; }
    }
}
