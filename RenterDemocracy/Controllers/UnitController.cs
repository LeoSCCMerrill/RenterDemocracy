using Microsoft.AspNetCore.Mvc;
using RenterDemocracy.Data;

namespace RenterDemocracy.Controllers
{
    public class UnitController : Controller
    {
        private readonly ApplicationDbContext _context;
        public UnitController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
