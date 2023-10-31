using Microsoft.AspNetCore.Mvc;
using RenterDemocracy.Data;

namespace RenterDemocracy.Controllers
{
    public class PostController : Controller
    {
        private readonly ApplicationDbContext _context;
        public PostController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
