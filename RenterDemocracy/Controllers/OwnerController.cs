using Microsoft.AspNetCore.Mvc;
using RenterDemocracy.Data;

namespace RenterDemocracy.Controllers
{
    public class OwnerController : Controller
    {
        private readonly ApplicationDbContext _context;
        public OwnerController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
