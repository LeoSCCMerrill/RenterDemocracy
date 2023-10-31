using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RenterDemocracy.Models;

namespace RenterDemocracy.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<House> Houses { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<RoomInviteCode> RoomInviteCodes { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<DirectMessage> DirectMessages { get; set; }
        public DbSet<VotingIssue> VotingIssues { get; set; }
        public DbSet<UserUnit> UserUnits { get; set; }
        public DbSet<UnitParking> UnitParkings { get; set; }
        public DbSet<VotingIssueVotes> VotingIssuesVotes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<UserUnit>().HasKey(uu => new {uu.UnitId, uu.UserId});
        }
    }
}