using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RenterDemocracy.Data;
using RenterDemocracy.Models;
using System.Security.Claims;

namespace RenterDemocracy.Controllers
{
    public class UnitController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UnitController(ApplicationDbContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            User currentUser = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            IList<UserUnit> userUnits = _context.UserUnits.Include(uu => uu.Unit).Where(uu => uu.UserId == currentUser.Id).ToList();
            userUnits.Concat(_context.UserUnits.Include(uu => uu.Unit).Where(uu => uu.UserId == currentUser.Id).ToList());
            return View();
        }
    }
}
