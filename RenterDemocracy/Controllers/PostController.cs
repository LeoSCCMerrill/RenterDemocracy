using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
using RenterDemocracy.Data;
using RenterDemocracy.Models;
using System.Security.Claims;

namespace RenterDemocracy.Controllers
{
    public class PostController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        public PostController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            KeyValuePair<Unit, IList<Post>> postsKVP = new KeyValuePair<Unit, IList<Post>>();
            Unit? unit = _context.Houses.Where(h => h.Id == userId).FirstOrDefault();
            unit ??= _context.Apartments.Where(h => h.Id == userId).FirstOrDefault();
            if (unit != null)
            {
                IDictionary<Unit, IList<Post>> postsDict = new Dictionary<Unit, IList<Post>>();
                IList<Post> postsList = _context.Posts.Where(p => p.Unit.Id == unit.Id).ToList();
                postsDict[unit] = postsList;
                foreach (KeyValuePair<Unit, IList<Post>> post in postsDict)
                {
                    postsKVP = post;
                }
            }
            return View(postsKVP);
        }
    }
}
