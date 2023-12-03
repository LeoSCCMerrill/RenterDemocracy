using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RenterDemocracy.Data;
using RenterDemocracy.Models;

namespace RenterDemocracy.Controllers
{

    [Authorize(Roles = nameof(RolesEnum.ADMINISTRATOR))]
    public class AdminController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AdminController> _logger;
        public AdminController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context, ILogger<AdminController> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            return View(await GetViewModel());
        }

        [HttpGet]
        public IActionResult CreateAdmin()
        {
            return View(new User());
        }
        [HttpPost]
        public async Task<IActionResult> CreateAdmin(User user)
        {
            user.EmailConfirmed = true;
            user.Email = user.UserName;
            user.NormalizedEmail = user.NormalizedUserName;
            await _userManager.CreateAsync(user, user.PasswordHash);
            await _userManager.AddToRoleAsync(user, RolesEnum.ADMINISTRATOR.ToString());
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult CreateOwner()
        {
            return View(new User());
        }
        [HttpPost]
        public async Task<IActionResult> CreateOwner(User user)
        {
            user.EmailConfirmed = true;
            user.Email = user.UserName;
            user.NormalizedEmail = user.NormalizedUserName;
            await _userManager.CreateAsync(user, user.PasswordHash);
            await _userManager.AddToRoleAsync(user, RolesEnum.OWNER.ToString());
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            await _userManager.DeleteAsync(await _userManager.FindByIdAsync(id));
            return RedirectToAction("Index");
        }
        private async Task<UserViewModel> GetViewModel()
        {
            IList<User> users = new List<User>();
            foreach (User user in _userManager.Users)
            {
                user.RoleNames = await _userManager.GetRolesAsync(user);
                users.Add(user);
            }
            UserViewModel model = new()
            {
                Users = users,
                Roles = _roleManager.Roles.ToList()
            };
            return model;
        }
    }
}
