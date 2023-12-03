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
    public class VotingController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        public VotingController(ApplicationDbContext context, UserManager<User> userManager)
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
            if (unit == null)
            {
                return RedirectToAction("Index", "Home");
            }
            IList<VotingIssue> votingIssues = _context.VotingIssues.Include(vi => vi.User).Include(vi => vi.Comments.OrderByDescending(c => c.Time)).Include(vi => vi.VotingIssueVotes).ThenInclude(viv => viv.User).Where(p => p.Unit.Id == unit.Id).OrderByDescending(p => p.Time).ToList();
            IList<VotingIssue> openVotingIssues = new List<VotingIssue>();
            IList<VotingIssue> completedVotingIssues = new List<VotingIssue>();
            foreach(VotingIssue votingIssue in votingIssues)
            {
                if(votingIssue.Completed)
                {
                    completedVotingIssues.Add(votingIssue);
                } else
                {
                    openVotingIssues.Add(votingIssue);
                }
            }
            return View(new VotingIssueViewModel()
            {
                OpenVotingIssues = openVotingIssues,
                CompletedVotingIssues = completedVotingIssues
            });

        }

        public async Task<IActionResult> CreateVotingIssue(string title, string content)
        {
            if (content != null && content != "" && title != null && title != "")
            {
                User? user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
                if (user != null)
                {
                    Unit? unit = _context.Units.FirstOrDefault(u => u.Users.Contains(user));
                    if (unit != null)
                    {
                        _context.VotingIssues.Add(new()
                        {
                            User = user,
                            Unit = unit,
                            Comments = new List<Comment>(),
                            Content = content,
                            Time = DateTime.Now,
                            Title = title,
                        });
                        _context.SaveChanges();
                    }
                }
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ReplyToVotingIssue(string content, string votingIssueId)
        {
            VotingIssue? votingIssue = _context.VotingIssues.Include(vi => vi.User).Include(vi => vi.Comments).FirstOrDefault(vi => vi.Id == votingIssueId);
            User? user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (votingIssue != null && votingIssue.Unit != null && votingIssue.User != null)
            {
                _context.Comments.Add(new()
                {
                    User = user,
                    Post = votingIssue,
                    Content = content,
                });
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Vote(string id, Votes vote)
        {
            VotingIssue? votingIssue = _context.VotingIssues.Include(vi=>vi.Unit).ThenInclude(u=>u.Users).Include(vi => vi.VotingIssueVotes).ThenInclude(viv => viv.User).FirstOrDefault(vi => vi.Id == id);
            User? user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (votingIssue != null && user != null)
            {
                votingIssue.VotingIssueVotes.Add(new()
                {
                    User = user,
                    Vote = vote,
                    VotingIssue = votingIssue,
                    UserId = user.Id,
                    VotingIssueId = id
                });
                if (votingIssue.VotingIssueVotes.Count == votingIssue.Unit.Users.Count)
                {
                    votingIssue.Completed = true;
                }
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
