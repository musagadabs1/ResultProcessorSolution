using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResultProcessor.Models;
using ResultProcessor.ViewModels;

namespace ResultProcessor.Controllers
{
    [Authorize(Roles ="Admin")]
    public class ManageUsersController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public ManageUsersController(UserManager<IdentityUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task< IActionResult> Index()
        {
            var adminUsers = (await _userManager.GetUsersInRoleAsync(Constants.Administrator)).ToArray();
            var everyUser = await _userManager.Users.ToArrayAsync();
            var roles = await _roleManager.Roles.ToArrayAsync();

            var model = new ManageUserViewModel
            {
                Administrators=adminUsers,
                Everyone=everyUser,
                Roles=roles
            };
            return View(model);
        }
    }
}