using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RenterDemocracy.Data;
using RenterDemocracy.Models;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace RenterDemocracy.Controllers
{
    public class InviteController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<InviteController> _logger;

        public InviteController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context, ILogger<InviteController> logger, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            User? user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            Invite? invite = _context.Invites.Include(i => i.ToUnit).Include(i => i.FromUser).FirstOrDefault(i => i.ToUserEmail == user.Email);
            return View(invite);
        }

        [HttpPost]
        public async Task<IActionResult> AcceptInvite(string id, bool accepted)
        {
            Invite? invite = _context.Invites.Include(i => i.ToUnit).Include(i => i.FromUser).FirstOrDefault(i => i.Id == id);
            if (invite == null)
            {
                return RedirectToAction("Index");
            }
            User? user = await _userManager.FindByEmailAsync(invite.ToUserEmail);
            if (user == null)
            {
                return RedirectToAction("Index");
            }
            if (accepted)
            {
                invite.ToUnit.UserUnits.Add(new()
                {
                    User = user,
                    UserId = user.Id,
                    Unit = invite.ToUnit,
                    UnitId = invite.ToUnit.Id,
                });
                invite.ToUnit.Users.Add(user);
                await _userManager.AddToRoleAsync(user, RolesEnum.HOUSE_MEMBER.ToString());
                _context.Invites.Remove(invite);
                _context.SaveChanges();
                await _signInManager.SignOutAsync();
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Unit");
            }
            else
            {
                _context.Invites.Remove(invite);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}
