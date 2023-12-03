using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RenterDemocracy.Models;
using System.Reflection.Emit;

namespace RenterDemocracy.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<User> ApplicationUsers { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<VotingIssue> VotingIssues { get; set; }
        public DbSet<UserUnit> UserUnits { get; set; }
        public DbSet<VotingIssueVotes> VotingIssuesVotes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Invite> Invites { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>().HasKey(x => x.Id);
            builder.Entity<UserUnit>()
                .HasKey(uu => new { uu.UnitId, uu.UserId });
            builder.Entity<UserUnit>()
                .HasOne(uu => uu.Unit)
                .WithMany(u => u.UserUnits)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<User>()
                .HasMany(u => u.Units)
                .WithMany(u => u.Users)
                .UsingEntity<UserUnit>();
            builder.Entity<User>()
                .HasMany(u => u.OwnedUnits)
                .WithOne(u => u.Owner);
            builder.Entity<VotingIssueVotes>()
                .HasKey(viv => new { viv.VotingIssueId, viv.UserId });
            builder.Entity<VotingIssueVotes>()
                .HasOne(viv => viv.VotingIssue)
                .WithMany(vi => vi.VotingIssueVotes)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<VotingIssue>()
                .HasMany(vi => vi.VotingUsers)
                .WithMany(u => u.VotingIssues)
                .UsingEntity<VotingIssueVotes>();
            

            const string OWNER_ID = "5cb99a62-bceb-4b4a-98d7-b250d8d7ae11";
            const string ADMIN_ID = "dc4ba651-0a2e-4d5b-8ae0-9f37fed328b6";
            const string USER_ID1 = "77529ac5-5363-45eb-8aa3-66e3ed9d7744";
            const string USER_ID2 = "f0eea591-7de0-4c86-8225-4f17e2423623";
            const string USER_ID3 = "d784ce2e-9e7c-4187-98b4-a1eb9e29ebed";
            const string USER_ID4 = "9649f0a0-bda2-4858-816a-46e673e066c0";
            const string USER_ID5 = "390aaa17-f40c-4c50-b7ab-1688a8c8f0e7";
            const string OWNER_ROLE_ID = "c42e3622-2385-4f71-83ba-9df25e51cb34";
            const string ADMIN_ROLE_ID = "680889e4-52bd-40e3-96bb-db110032a501";
            const string HOUSE_MEM_ROLE_ID = "9f1c7977-4d6d-457d-9e42-4a1c0fdfbba9";
            builder.Entity<IdentityRole>().HasData(
            new IdentityRole
            {
                Id = OWNER_ROLE_ID,
                Name = RolesEnum.OWNER.ToString(),
                NormalizedName = RolesEnum.OWNER.ToString().Normalize(),
            },
            new IdentityRole
            {
                Id = ADMIN_ROLE_ID,
                Name = RolesEnum.ADMINISTRATOR.ToString(),
                NormalizedName = RolesEnum.ADMINISTRATOR.ToString().Normalize(),
            },
            new IdentityRole
            {
                Id = HOUSE_MEM_ROLE_ID,
                Name = RolesEnum.HOUSE_MEMBER.ToString(),
                NormalizedName = RolesEnum.HOUSE_MEMBER.ToString().Normalize(),
            });
            builder.Entity<User>().HasData(
            new User
            {
                Id = OWNER_ID,
                UserName = "owner@email.com",
                NormalizedUserName = "OWNER@EMAIL.COM",
                Email = "owner@email.com",
                NormalizedEmail = "OWNER@email.COM",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAEAACcQAAAAEALfXOO0MYDpnaWi+2TO6u67hE3xzrew03QVb8Vb3wTOdiKZzWGSm42SscHBRPRT0g==", // Password1!
                SecurityStamp = "VVPCRDAS3MJWQD5CSW2GWPRADBXEZINA",
                ConcurrencyStamp = "c8554266-b401-4519-9aeb-a9283053fc58"
            }, new User
            {
                Id = ADMIN_ID,
                UserName = "admin@email.com",
                NormalizedUserName = "ADMIN@EMAIL.COM",
                Email = "admin@email.com",
                NormalizedEmail = "ADMIN@EMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAEAACcQAAAAEALfXOO0MYDpnaWi+2TO6u67hE3xzrew03QVb8Vb3wTOdiKZzWGSm42SscHBRPRT0g==", // Password1!
                SecurityStamp = "1",
                ConcurrencyStamp = "1"
            }, new User
            {
                Id = USER_ID1,
                UserName = "user1@email.com",
                NormalizedUserName = "USER1@EMAIL.COM",
                Email = "user1@email.com",
                NormalizedEmail = "USER1@EMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAEAACcQAAAAEALfXOO0MYDpnaWi+2TO6u67hE3xzrew03QVb8Vb3wTOdiKZzWGSm42SscHBRPRT0g==", // Password1!
                SecurityStamp = "VVPCRDAS3MJWQD5CSW2GWPRADBXEZINA",
                ConcurrencyStamp = "c8554266-b401-4519-9aeb-a9283053fc58"
            }, new User
            {
                Id = USER_ID2,
                UserName = "user2@email.com",
                NormalizedUserName = "USER2@EMAIL.COM",
                Email = "user2@email.com",
                NormalizedEmail = "USER2@EMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAEAACcQAAAAEALfXOO0MYDpnaWi+2TO6u67hE3xzrew03QVb8Vb3wTOdiKZzWGSm42SscHBRPRT0g==", // Password1!
                SecurityStamp = "VVPCRDAS3MJWQD5CSW2GWPRADBXEZINA",
                ConcurrencyStamp = "c8554266-b401-4519-9aeb-a9283053fc58"
            }, new User
            {
                Id = USER_ID3,
                UserName = "user3@email.com",
                NormalizedUserName = "USER3@EMAIL.COM",
                Email = "user3@email.com",
                NormalizedEmail = "USER3@EMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAEAACcQAAAAEALfXOO0MYDpnaWi+2TO6u67hE3xzrew03QVb8Vb3wTOdiKZzWGSm42SscHBRPRT0g==", // Password1!
                SecurityStamp = "VVPCRDAS3MJWQD5CSW2GWPRADBXEZINA",
                ConcurrencyStamp = "c8554266-b401-4519-9aeb-a9283053fc58"
            }, new User
            {
                Id = USER_ID4,
                UserName = "user4@email.com",
                NormalizedUserName = "USER4@EMAIL.COM",
                Email = "user4@email.com",
                NormalizedEmail = "USER4@EMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAEAACcQAAAAEALfXOO0MYDpnaWi+2TO6u67hE3xzrew03QVb8Vb3wTOdiKZzWGSm42SscHBRPRT0g==", // Password1!
                SecurityStamp = "VVPCRDAS3MJWQD5CSW2GWPRADBXEZINA",
                ConcurrencyStamp = "c8554266-b401-4519-9aeb-a9283053fc58"
            }, new User
            {
                Id = USER_ID5,
                UserName = "user5@email.com",
                NormalizedUserName = "USER5@EMAIL.COM",
                Email = "user5@email.com",
                NormalizedEmail = "USER5@EMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAEAACcQAAAAEALfXOO0MYDpnaWi+2TO6u67hE3xzrew03QVb8Vb3wTOdiKZzWGSm42SscHBRPRT0g==", // Password1!
                SecurityStamp = "VVPCRDAS3MJWQD5CSW2GWPRADBXEZINA",
                ConcurrencyStamp = "c8554266-b401-4519-9aeb-a9283053fc58"
            });
            builder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
            {
                RoleId = OWNER_ROLE_ID,
                UserId = OWNER_ID
            }, new IdentityUserRole<string>
            {
                RoleId = ADMIN_ROLE_ID,
                UserId = ADMIN_ID
            });
        }
    }
}