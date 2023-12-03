using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RenterDemocracy.Data;
using RenterDemocracy.Models;
using RenterDemocracy.Util;
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
            User user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            Unit? unit = _context.Units.Include(u=>u.UserUnits).ThenInclude(uu=>uu.User).Where(u=>u.Users.Contains(user)).FirstOrDefault();
            if (unit == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(unit);
            }
        }

        public async Task<IActionResult> LeaveUnit()
        {
            User user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            Unit? unit = _context.Units.Include(u => u.UserUnits).ThenInclude(uu => uu.User).Where(u => u.Users.Contains(user)).FirstOrDefault();
            if (unit == null || user == null)
            {
                return RedirectToAction("Index");
            }
            UnitUtil.RemoveTenantFromUnit(user, unit, _userManager);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult AddTenant(string unitId)
        {
            return View(unitId);
        }

        [HttpPost]
        public async Task<IActionResult> AddTenant(string unitId, string email)
        {
            User? user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (user == null)
            {
                return RedirectToAction("Index");
            }
            UnitUtil.AddTenantToUnit(email, unitId, _context, user);
            return RedirectToAction("Index");
        }
    }
}
