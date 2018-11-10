using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResultProcessor.Models;
using ResultProcessor.ViewModels;

namespace ResultProcessor.Controllers
{
    public class ManageUsersController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        public ManageUsersController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task< IActionResult> Index()
        {
            var adminUsers = (await _userManager.GetUsersInRoleAsync(Constants.Administrator)).ToArray();
            var everyUser = await _userManager.Users.ToArrayAsync();

            var model = new ManageUserViewModel
            {
                Administrators=adminUsers,
                Everyone=everyUser
            };
            return View(model);
        }
    }
}