using GestionNonconformite_NCARouiba.Models.Entities;
using GestionNonconformite_NCARouiba.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GestionNonconformite_NCARouiba.Controllers
{
    public class MyAccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly string _defaultUserAvatar;

        public MyAccountController(UserManager<User> userManager, RoleManager<Role> roleManager, IHostingEnvironment hostingEnvironment)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _hostingEnvironment = hostingEnvironment;
            _defaultUserAvatar = "default.png";
        }

        [HttpGet]
        public async Task<IActionResult> DetailAsync(string Id)
        {
            User user = await _userManager.FindByIdAsync(Id);
            DetailsUserViewModel userDetails = new DetailsUserViewModel()
            {
                User = user,
                UserRoles = await _userManager.GetRolesAsync(user)
            };

            return View(userDetails);
        }

        [HttpGet]
        public async Task<IActionResult> EditAsync(string Id)
        {
            User user = await _userManager.FindByIdAsync(Id);

            EditUserViewModel userEdit = new EditUserViewModel()
            {
                Nom = user.Nom,
                Prenom = user.Prenom,
                Fonction = user.Fonction,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                Adresse = user.Adresse,
                Id = user.Id
            };

            return View(userEdit);
        }
            

        public async Task<IActionResult> EditAsync(EditUserViewModel userEditViewModel)
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByIdAsync(userEditViewModel.Id);
                string uniqueFileName = user.PhotoPath;

                if (userEditViewModel.Photo != null)
                {
                    if (uniqueFileName != "default.png")
                    {
                        string photoPath = Path.Combine(_hostingEnvironment.WebRootPath, "images\\avatars", uniqueFileName);
                        System.IO.File.Delete(photoPath);
                    }
                    string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images\\avatars");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + userEditViewModel.Photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    userEditViewModel.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                }

                user.Nom = userEditViewModel.Nom;
                user.Prenom = userEditViewModel.Prenom;
                user.Fonction = userEditViewModel.Fonction;
                user.PhotoPath = uniqueFileName;
                user.PhoneNumber = userEditViewModel.PhoneNumber;
                user.Email = userEditViewModel.Email;
                user.Adresse = userEditViewModel.Adresse;
                user.UserName = userEditViewModel.Email;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    
                    if (userEditViewModel.Password != null)
                    {
                        var newPass = _userManager.PasswordHasher.HashPassword(user, userEditViewModel.Password);
                        user.PasswordHash = newPass;
                        user.Password = userEditViewModel.Password;
                        result = await _userManager.UpdateAsync(user);
                    }

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Detail",new { Id = user.Id });
                    }
                }
            }

            return View(userEditViewModel);
        }
    }
}
