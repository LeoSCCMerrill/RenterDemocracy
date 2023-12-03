using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RenterDemocracy.Data;
using RenterDemocracy.Models;
using System.Security.Claims;

namespace RenterDemocracy.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserController(ApplicationDbContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            User user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Index(User user)
        {
            User currentUser = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            currentUser.FirstName = user.FirstName;
            currentUser.MiddleName = user.MiddleName;
            currentUser.LastName = user.LastName;
            currentUser.PhoneNumber = user.PhoneNumber;
            currentUser.PhoneNumberConfirmed = true;
            currentUser.BirthDate = user.BirthDate;
            await _userManager.UpdateAsync(currentUser);
            return RedirectToAction("Index", "Home");
        }
    }
}
