using Microsoft.AspNetCore.Mvc;
using RenterDemocracy.Data;
using RenterDemocracy.Models;

namespace RenterDemocracy.Controllers
{
    public class OwnerController : Controller
    {
        private readonly ApplicationDbContext _context;
        public OwnerController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AddApartment()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddApartment(Apartment apartment)
        {
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult AddHouse()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddHouse(House house)
        {
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult AddTenentToApartment(int Id)
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddTenentToApartment(User user, Apartment apartment)
        {
            _context.UserUnits.Add(new UserUnit
            {
                UserId = user.Id,
                UnitId = apartment.Id
            });
            return RedirectToAction("ApartmentDetails", apartment.Id);
        }
        [HttpGet]
        [HttpPost]
        public IActionResult AddTenent(User user, Apartment apartment)
        {
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult AddTenent(User user, House house)
        {
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult ApartmentDetails(Apartment apartment)
        {
            return View(apartment);
        }
        [HttpGet]
        public IActionResult EditApartment(int Id)
        {
            return View(_context.Apartments.Find(Id));
        }
        [HttpPost]
        public IActionResult EditApartment(Apartment apartment)
        {
            _context.Apartments.Update(apartment);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult EditHouse(int Id) { 
            return View(_context.Houses.Find(Id));
        }
    }
}
