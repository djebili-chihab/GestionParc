using GestionNonconformite_NCARouiba.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GestionNonconformite_NCARouiba.ViewModels
{
    public class EditRoleViewModel
    {

        public EditRoleViewModel()
        {
            UsersInRole = new List<UserRoleViewModel>();
            AllUsers = new List<UserRoleViewModel>();
        }
        public string Id { get; set; }
        [Required]
        public string RoleName { get; set; }
        public string Description { get; set; }
        public List<UserRoleViewModel> UsersInRole { get; set; }
        public List<UserRoleViewModel> AllUsers { get; set; }
    }
}
