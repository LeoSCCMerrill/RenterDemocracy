using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
using RenterDemocracy.Data;
using RenterDemocracy.Models;
using System.Reflection;
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

        public async Task<IActionResult> Index()
        {
            User? user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            Unit? unit = _context.Units.FirstOrDefault(u => u.Users.Contains(user));
            if (unit != null)
            {
                IList<Post> tempPosts = _context.Posts.Include(p => p.User).Include(p => p.Comments.OrderBy(c=>c.Time)).Where(p => p.Unit.Id == unit.Id).OrderByDescending(p=>p.Time).ToList();
                IList<Post> posts = new List<Post>();
                foreach (Post post in tempPosts)
                {
                    if (post.GetType() != typeof(VotingIssue))
                    {
                        posts.Add(post);
                    }
                }
                return View(posts);
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> CreatePost(string title, string content)
        {
            if (content != null && content != "" && title != null && title != "")
            {
                User? user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
                if (user != null)
                {
                    Unit? unit = _context.Units.FirstOrDefault(u => u.Users.Contains(user));
                    if (unit != null)
                    {
                        _context.Posts.Add(new()
                        {
                            User = user,
                            Unit = unit,
                            Content = content,
                            Title = title,
                        });
                        _context.SaveChanges();
                    }
                }
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ReplyToPost(string content, string postId)
        {
            Post? post = _context.Posts.Include(p => p.User).Include(p => p.Unit).Include(p => p.Comments).FirstOrDefault(p => p.Id == postId);
            User? user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (post != null && post.Unit != null && post.User != null)
            {
                _context.Comments.Add(new()
                {
                    User = user,
                    Post = post,
                    Content = content,
                });
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult DeletePost(string id)
        {
            Post? post = _context.Posts.FirstOrDefault(p => p.Id == id);
            if (post != null)
            {
                _context.Posts.Remove(post);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public IActionResult DeleteComment(string id)
        {
            Comment? comment = _context.Comments.FirstOrDefault(c => c.Id == id);
            if (comment != null)
            {
                _context.Comments.Remove(comment);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
