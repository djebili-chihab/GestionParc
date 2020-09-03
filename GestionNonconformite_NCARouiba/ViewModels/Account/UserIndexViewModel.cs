using GestionNonconformite_NCARouiba.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionNonconformite_NCARouiba.ViewModels
{
    public class UserIndexViewModel
    {
        public User User { get; set; }
        public List<Role> UserRoles { get; set; }
    }
}
