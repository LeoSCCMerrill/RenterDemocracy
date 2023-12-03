using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;
using RenterDemocracy.Data;
using RenterDemocracy.Models;
using RenterDemocracy.Util;
using System.Security.Claims;

namespace RenterDemocracy.Controllers
{
    [Authorize(Roles = nameof(RolesEnum.OWNER))]
    public class OwnerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public OwnerController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            IList<Unit>? units = _context.Units.Include(u => u.UserUnits).ThenInclude(uu => uu.User).Where(u => u.Owner.Id == User.FindFirstValue(ClaimTypes.NameIdentifier)).ToList();
            return View(units);
        }

        [HttpGet]
        public IActionResult AddApartment()
        {
            return View(new Unit());
        }
        [HttpPost]
        public async Task<IActionResult> AddApartment(Unit apartment)
        {
            apartment.UnitType = UnitType.APARTMENT;
            apartment.Owner = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            _context.Units.Add(apartment);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult AddHouse()
        {
            return View(new Unit());
        }

        [HttpPost]
        public async Task<IActionResult> AddHouse(Unit house)
        {
            house.UnitType = UnitType.HOUSE;
            house.UnitNumber = null;
            house.Owner = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            _context.Units.Add(house);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult AddTenant(string id)
        {
            return View(_context.Units.Where(u => u.Id == id).FirstOrDefault());
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

        [HttpGet]
        public IActionResult EditApartment(string id)
        {
            return View(_context.Units.FirstOrDefault(u => u.Id == id));
        }
        [HttpPost]
        public IActionResult EditApartment(Unit apartment)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(apartment).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(apartment);
        }

        [HttpGet]
        public IActionResult EditHouse(string id)
        {
            return View(_context.Units.FirstOrDefault(u => u.Id == id));
        }

        [HttpPost]
        public IActionResult EditHouse(Unit house)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(house).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(house);
        }

        public async Task<IActionResult> DeleteTenant(string tenantId, string unitId)
        {
            Unit? unit = _context.Units.Include(u => u.UserUnits).ThenInclude(uu => uu.User).Where(u => u.Id == unitId).FirstOrDefault();
            User tenant = await _userManager.FindByIdAsync(tenantId);
            if (unit == null || tenant == null)
            {
                return RedirectToAction("Index");
            }
            UnitUtil.RemoveTenantFromUnit(tenant, unit, _userManager);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(string id)
        {
            Unit? unit = _context.Units.FirstOrDefault(u => u.Id == id);
            if (unit != null)
            {
                _context.Units.Remove(unit);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
