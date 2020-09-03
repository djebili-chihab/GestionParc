using GestionNonconformite_NCARouiba.Models.Entities;
using GestionNonconformite_NCARouiba.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionNonconformite_NCARouiba.Controllers
{
    public class AdministrationController : Controller
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        public AdministrationController(RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult RoleIndex()
        {
            var model = _roleManager.Roles;
            return View(model);
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoleAsync(CreateRoleViewModel createRoleViewModel)
        {
            if (ModelState.IsValid)
            {
                Role identityRole = new Role()
                {
                    Name = createRoleViewModel.RoleName,
                    Description = createRoleViewModel.Description
                };

                IdentityResult result = await _roleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                {
                    return RedirectToAction("roleIndex", "administration");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(createRoleViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> EditRoleAsync(string id)
        {
            Role role = await _roleManager.FindByIdAsync(id);

            if(role == null)
            {
                ViewBag.ErrorMessage = $"Role avec ID = {id} est introuvable !";
                return View("NotFound");
            }

            ViewBag.ListUsers = new List<UserRoleViewModel>();

            var users =  _userManager.Users.ToList<User>();

            foreach (User user in users)
            {
                Boolean result = await _userManager.IsInRoleAsync(user, role.Name);
                if (result)
                {
                    ViewBag.ListUsers.Add(new UserRoleViewModel()
                    {
                        Nom = user.Nom,
                        Prenom = user.Prenom,
                        Fonction = user.Fonction,
                        Id = user.Id,
                        Selected = result
                    });
                }      
            }

            return View(role);
        }

        [HttpPost]
        public async Task<IActionResult> EditRoleAsync(Role role, string id)
        {
            var roleUpdated = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role avec ID = {id} est introuvable !";
                return View("NotFound");
            }
            else
            {
                roleUpdated.Name = role.Name;
                roleUpdated.Description = role.Description;

                var result = await _roleManager.UpdateAsync(roleUpdated);

                if (result.Succeeded)
                {
                    return RedirectToAction("roleIndex", "administration");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(role);
            }

        }  

        [HttpPost]
        public async Task<IActionResult> RemoveUserFromRoleAsync(string idUser, string idRole)
        {
            User user = await _userManager.FindByIdAsync(idUser);
            Role role = await _roleManager.FindByIdAsync(idRole);
            var result = await _userManager.RemoveFromRoleAsync(user, role.Name);

            if (result.Succeeded)
            {
                return Redirect("/administration/editRole/" + idRole);
            }
            return View();
        }


    }
}
