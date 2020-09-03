using GestionNonconformite_NCARouiba.Models.Entities;
using GestionNonconformite_NCARouiba.ViewModels;
using Microsoft.AspNetCore.Authorization;
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
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly string _defaultUserAvatar ;

        public AccountController(UserManager<User> userManager, RoleManager<Role> roleManager, SignInManager<User> signInManager, IHostingEnvironment hostingEnvironment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _hostingEnvironment = hostingEnvironment;
            _defaultUserAvatar = "default.png";
            _roleManager = roleManager;
        }

        public async Task<ViewResult> IndexAsync()
        {
            List<User> users = _userManager.Users.ToList<User>();
            List<Role> roles = _roleManager.Roles.ToList<Role>();

            List<UserIndexViewModel> model = new List<UserIndexViewModel>();

            foreach(User user in users)
            {
                List<Role> userRoles = new List<Role>();

                foreach(Role role in roles)
                {
                    Boolean result = await _userManager.IsInRoleAsync(user, role.Name);
                    if (result)
                    {
                        userRoles.Add(role);
                    }
                }

                UserIndexViewModel userIndex = new UserIndexViewModel()
                {
                    User = user,
                    UserRoles = userRoles
                };

                model.Add(userIndex);
            }
            return View(model);
        }

        [HttpGet]
        public ViewResult Create()
        {
            List<Role> roles = _roleManager.Roles.ToList<Role>();
            ViewBag.Roles = roles;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(UserCreateViewModel userCreateViewModel)
        {

            if (ModelState.IsValid)
            {
                string uniqueFileName = _defaultUserAvatar;

                if (userCreateViewModel.Photo != null)
                {
                    string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images\\avatars");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + userCreateViewModel.Photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    userCreateViewModel.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                }

                User user = new User()
                {
                    Nom = userCreateViewModel.Nom,
                    Prenom = userCreateViewModel.Prenom,
                    Fonction = userCreateViewModel.Fonction,
                    PhotoPath = uniqueFileName,
                    PhoneNumber = userCreateViewModel.PhoneNumber,
                    Email = userCreateViewModel.Email,
                    Adresse = userCreateViewModel.Adresse,
                    UserName = userCreateViewModel.Email,
                    Password = userCreateViewModel.Password,
                };

                var result = await _userManager.CreateAsync(user, userCreateViewModel.Password);

                if (result.Succeeded)
                {
                    User userStored = await _userManager.FindByNameAsync(user.UserName);
                    foreach (SelectRolesViewModel roleSelected in userCreateViewModel.UserRoles)
                    {
                        if (roleSelected.Selected)
                        {
                            var resultat = await _userManager.AddToRoleAsync(userStored, roleSelected.RoleName);
                        }
                    }

                    return RedirectToAction("index", "Account");
                }

                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(userCreateViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> EditAsync(String id)
        {
            User user = await _userManager.FindByIdAsync(id);
            List<Role> roles = _roleManager.Roles.ToList<Role>();
            List<SelectRolesViewModel> rolesSelected = new List<SelectRolesViewModel>();
            ViewBag.UserId = id;
      
            foreach(Role role in roles)
            {
                Boolean selected = await _userManager.IsInRoleAsync(user, role.Name);
                SelectRolesViewModel roleSelected = new SelectRolesViewModel()
                {
                    Id = role.Id,
                    RoleName = role.Name,
                    Selected = selected
                };

                rolesSelected.Add(roleSelected);
            }

            EditUserViewModel userEdit = new EditUserViewModel()
            {
                Nom = user.Nom,
                Prenom = user.Prenom,
                Fonction = user.Fonction,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                UserRoles = rolesSelected
            };

            return View(userEdit);
        }

        [HttpPost]
        public async Task<IActionResult> EditAsync(EditUserViewModel userEditViewModel, string id)
        {

            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByIdAsync(id);
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
                    foreach (SelectRolesViewModel role in userEditViewModel.UserRoles)
                    {
                        if (role.Selected && ! await _userManager.IsInRoleAsync(user,role.RoleName))
                        {
                            await _userManager.AddToRoleAsync(user, role.RoleName);

                        }else if (! role.Selected && await _userManager.IsInRoleAsync(user, role.RoleName))
                        {
                            await _userManager.RemoveFromRoleAsync(user, role.RoleName);
                        }
                    }

                    if (userEditViewModel.Password != null)
                    {
                        result = await _userManager.AddPasswordAsync(user, userEditViewModel.Password);
                    }

                    if (result.Succeeded)
                    {
                        return RedirectToAction("index", "Account");
                    }
                }
            }     
                
            return View(userEditViewModel);
        }

        [HttpGet]
        public async Task<JsonResult> DetailsAsync(string Id)
        {
            User user = await _userManager.FindByIdAsync(Id);
            DetailsUserViewModel userDetails = new DetailsUserViewModel()
            {
                User = user,
                UserRoles = await _userManager.GetRolesAsync(user)
            };

            return new JsonResult(userDetails);
        }

       
        public async Task<IActionResult> DeleteAsync(string id)
        {
            User user = await _userManager.FindByIdAsync(id);

            if(user == null)
            {
                ViewBag.ErrorMessage = $"user with id = {id} cannot be found !";
                return View("NotFound");
            }
            else
            {
                string uniqueFileName = user.PhotoPath;
                var result = await _userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    if (!uniqueFileName.Equals(_defaultUserAvatar))
                    {
                        string photoPath = Path.Combine(_hostingEnvironment.WebRootPath, "images\\avatars", uniqueFileName);
                        System.IO.File.Delete(photoPath);
                    }
                    
                    return RedirectToAction("index", "Account");
                }

                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }


            return View("index");
        }


        [HttpPost]
        public async Task<IActionResult> LogoutAsync()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("login", "Account");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [AcceptVerbs("Get","Post")]
        [AllowAnonymous]
        public async Task<IActionResult> IsEmailInUser(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            
            if(user == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"Email {email} est déja utilisé");
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync(LogingViewModel logingViewModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                User user = new User()
                {
                    Email = logingViewModel.Email,
                    UserName = logingViewModel.Email,
                };

                var result = await _signInManager.PasswordSignInAsync(user.UserName, logingViewModel.Password, logingViewModel.RemenberMe, false);

                if (result.Succeeded)
                {
                    User userLoged = await _userManager.FindByNameAsync(user.Email);
                    ViewBag.UserLoged = userLoged;
                        
                    if(!string.IsNullOrEmpty(returnUrl))
                    {
                        return LocalRedirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("index", "home");
                    }
                }

                ModelState.AddModelError("", "Mot de passe ou email incorrect");
                
            }
            return View();
        }
    }
}
