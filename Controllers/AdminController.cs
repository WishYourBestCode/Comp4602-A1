using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Blog.Models;
using Blog.ViewModels; // For AdminUserViewModel

namespace Blog.Controllers
{
    // [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<CustomUser> _userManager;
        private readonly RoleManager<CustomRole> _roleManager;

        public AdminController(UserManager<CustomUser> userManager, RoleManager<CustomRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: /Admin/Index
        public async Task<IActionResult> Index()
        {
            var allUsers = _userManager.Users.ToList();
            var model = new List<AdminUserViewModel>();

            foreach (var user in allUsers)
            {
                var roles = await _userManager.GetRolesAsync(user); 

                model.Add(new AdminUserViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    IsApproved = user.IsApproved,
                    Roles = roles
                });
            }

            return View(model.OrderByDescending(u => u.Roles.Contains("Admin"))       
                    .ThenByDescending(u => u.Roles.Contains("Contributor"))   
                    .ThenBy(u => u.Email)                                        
                    .ToList());
        }

        [HttpPost]
        public async Task<IActionResult> Approve(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                user.IsApproved = !user.IsApproved;
                await _userManager.UpdateAsync(user);
            }
            return RedirectToAction(nameof(Index));
        }

   
       [HttpPost]
        public async Task<IActionResult> AssignRole(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null && await _roleManager.RoleExistsAsync(roleName))
            {
                var currentRoles = await _userManager.GetRolesAsync(user);

                if (currentRoles.Any())
                {
                    await _userManager.RemoveFromRolesAsync(user, currentRoles);
                }

                await _userManager.AddToRoleAsync(user, roleName);
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
