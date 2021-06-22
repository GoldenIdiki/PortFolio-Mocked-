using GoldPortFolio.Data;
using GoldPortFolio.Models;
using GoldPortFolio.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoldPortFolio.Controllers
{
    public class ServiceController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationDbContext _db;
        private readonly UpdateViewModel _updateView;
        private readonly IProfileRepository _profileRepository;

        public ServiceController(UserManager<IdentityUser> userManager,
                                      SignInManager<IdentityUser> signInManager,
                                        ApplicationDbContext db, IProfileRepository profileRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
            _profileRepository = profileRepository;
            _updateView = new UpdateViewModel();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                Address address = new Address
                {
                    Street = model.StreetName,
                    State = model.State,
                    City = model.City,
                    Country = model.Country,
                };

                WorkExperience workexperience = new WorkExperience
                {
                    YearStarted = model.YearStarted,
                    YearEnded = model.YearEnded,
                    CompanyName = model.CompanyName,
                    JobTitle = model.JobTitle,
                    JobDescription = model.JobDescription,
                };

                var user = new Profile
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    Password = model.Password,
                    PasswordHash = model.Password,
                    Profession = model.Profession,
                    Qualification = model.Qualification,
                    LinkInUrl = model.LinkedInUrl,
                    GitHubUrl = model.GitHubUrl,
                    UserName = model.Email,
                    Email = model.Email,
                };

                user.Address.Add(address);
                user.WorkExperience.Add(workexperience);

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    return RedirectToAction("index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel user)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(user.Email, user.Password, user.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

            }
            return View(user);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Index()
        {
            UpdateViewModel update = new UpdateViewModel();
            var user = _profileRepository.GetProfile();


            update.Id = user.Id;
            update.FirstName = user.FirstName;
            update.LastName = user.LastName;
            update.Email = user.Email;
            update.PhoneNumber = user.PhoneNumber;
            update.Profession = user.Profession;
            update.Qualification = user.Qualification;
            update.LinkedInUrl = user.LinkInUrl;
            update.GitHubUrl = user.GitHubUrl;
            update.StreetName = user.Address[0].Street;
            update.State = user.Address[0].State;
            update.City = user.Address[0].City;
            update.Country = user.Address[0].Country;
            update.CompanyName = user.WorkExperience[0].CompanyName;
            update.YearStarted = user.WorkExperience[0].YearStarted;
            update.YearEnded = user.WorkExperience[0].YearEnded;
            update.JobTitle = user.WorkExperience[0].JobTitle;
            update.JobDescription = user.WorkExperience[0].JobDescription;

            return View(update);
        }

        [HttpPost]
        public IActionResult Index(UpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _db.ProfileTbl.FirstOrDefault();
                var address = _db.AddressTbl.FirstOrDefault(x => x.ProfileId == user.Id);
                var workExperience = _db.WorkExperienceTbl.FirstOrDefault(x => x.ProfileId == user.Id);

                user.Id = model.Id;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.PhoneNumber = model.PhoneNumber;
                user.Profession = model.Profession;
                user.Qualification = model.Qualification;
                user.LinkInUrl = model.LinkedInUrl;
                user.GitHubUrl = model.GitHubUrl;

                address.ProfileId = model.Id;
                address.Street = model.StreetName;
                address.State = model.State;
                address.City = model.City;
                address.Country = model.Country;

                workExperience.ProfileId = model.Id;
                workExperience.CompanyName = model.CompanyName;
                workExperience.YearStarted = model.YearStarted;
                workExperience.YearEnded = model.YearEnded;
                workExperience.JobTitle = model.JobTitle;
                workExperience.JobDescription = model.JobDescription;


                _db.SaveChanges();

                return RedirectToAction("index", "Home");
                //var result = await _userManager.UpdateAsync(user);

                //if (result.Succeeded)
                //{
                //    await _signInManager.SignInAsync(user, isPersistent: false);

                //    return RedirectToAction("index", "Home");
                //}
            }
            return View(model);
            
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Login");
        }
    }
}
