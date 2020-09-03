using GestionNonconformite_NCARouiba.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionNonconformite_NCARouiba.ViewModels
{
    public class SelectRolesViewModel
    {
        public string Id { get; set; }
        public string RoleName { get; set; }
        public Boolean Selected { get; set; }
    }
}
