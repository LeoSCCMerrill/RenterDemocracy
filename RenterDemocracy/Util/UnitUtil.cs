using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RenterDemocracy.Data;
using RenterDemocracy.Models;
using System.Security.Claims;

namespace RenterDemocracy.Util
{
    public class UnitUtil
    {
        public async static void RemoveTenantFromUnit(User user, Unit unit, UserManager<User> _userManager)
        {
            unit.Users.Remove(user);
            user.Units.Remove(unit);
            await _userManager.RemoveFromRoleAsync(user, RolesEnum.HOUSE_MEMBER.ToString());
            foreach (UserUnit userUnit in unit.UserUnits)
            {
                if (userUnit.UserId == unit.Id)
                {
                    unit.UserUnits.Remove(userUnit);
                }
            }
        }

        public static bool AddTenantToUnit(string email, string unitId, ApplicationDbContext _context, User user)
        {
            Unit? unit = _context.Units.FirstOrDefault(u => u.Id == unitId);
            if (unit == null)
            {
                return false;
            }
            _context.Invites.Add(new Invite
            {
                ToUnit = unit,
                FromUser = user,
                ToUserEmail = email,
            });
            _context.SaveChanges();
            return true;
        }
    }
}
