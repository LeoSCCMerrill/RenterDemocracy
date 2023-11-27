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
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<UserUnit>().HasKey(uu => new {uu.UnitId, uu.UserId});
            builder.Entity<UserUnit>().HasOne(uu => uu.Unit).WithMany(u => u.UserUnits).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<User>().HasKey(x => x.Id);
            builder.Entity<DirectMessage>().HasOne(dm => dm.ToUser).WithMany(u =>  u.DirectMessagesReceived).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<User>()
                .HasMany(u => u.Units)
                .WithMany(u => u.Users)
                .UsingEntity<UserUnit>();
            const string OWNER_ID = "5cb99a62-bceb-4b4a-98d7-b250d8d7ae11";
            const string PROP_MAN_ID = "8acb6e32-4bff-407f-9842-b477c54ecfed";
            const string ADMIN_ID = "dc4ba651-0a2e-4d5b-8ae0-9f37fed328b6";
            const string EXEC_ID = "b4280b6a-0613-4cbd-a9e6-f1701e926e73";
            const string LEGIS_CHAIR_ID = "22d6208e-e968-487e-a8f6-59a1c3ce94d7";
            const string LEGIS_MEM_ID = "092586f9-483d-4085-9b17-829c5caccd91";
            const string HOUSE_MEM_ID = "77529ac5-5363-45eb-8aa3-66e3ed9d7744";
            const string OWNER_ROLE_ID = "c42e3622-2385-4f71-83ba-9df25e51cb34";
            const string PROP_MAN_ROLE_ID = "92d96b45-16af-4461-8c3e-76821fa3f56b";
            const string ADMIN_ROLE_ID = "680889e4-52bd-40e3-96bb-db110032a501";
            const string EXEC_ROLE_ID = "942ea005-0383-4dc7-b2af-2f9d8fb59376";
            const string LEGIS_CHAIR_ROLE_ID = "83edd07a-2018-4309-a8a1-5d2c23e61f84";
            const string LEGIS_MEM_ROLE_ID = "b8b6ed3c-8c6a-443b-95c7-52d11bbe8129";
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
                Id = PROP_MAN_ROLE_ID,
                Name = RolesEnum.PROPERTY_MANAGER.ToString(),
                NormalizedName = RolesEnum.PROPERTY_MANAGER.ToString().Normalize(),
            },
            new IdentityRole
            {
                Id = ADMIN_ROLE_ID,
                Name = RolesEnum.ADMINISTRATOR.ToString(),
                NormalizedName = RolesEnum.ADMINISTRATOR.ToString().Normalize(),
            },
            new IdentityRole
            {
                Id = EXEC_ROLE_ID,
                Name = RolesEnum.EXECUTIVE.ToString(),
                NormalizedName = RolesEnum.EXECUTIVE.ToString().Normalize(),
            },
            new IdentityRole
            {
                Id = LEGIS_CHAIR_ROLE_ID,
                Name = RolesEnum.LEGISLATOR_CHAIR.ToString(),
                NormalizedName = RolesEnum.LEGISLATOR_CHAIR.ToString().Normalize(),
            },
            new IdentityRole
            {
                Id = LEGIS_MEM_ROLE_ID,
                Name = RolesEnum.LEGISLATOR_MEMBER.ToString(),
                NormalizedName = RolesEnum.LEGISLATOR_MEMBER.ToString().Normalize(),
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
                Id = PROP_MAN_ID,
                UserName = "manager@email.com",
                NormalizedUserName = "MANAGER@EMAIL.COM",
                Email = "manager@email.com",
                NormalizedEmail = "MANAGER@EMAIL.COM",
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
                Id = EXEC_ID,
                UserName = "exec@email.com",
                NormalizedUserName = "EXEC@EMAIL.COM",
                Email = "exec@email.com",
                NormalizedEmail = "EXEC@EMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAEAACcQAAAAEALfXOO0MYDpnaWi+2TO6u67hE3xzrew03QVb8Vb3wTOdiKZzWGSm42SscHBRPRT0g==", // Password1!
                SecurityStamp = "VVPCRDAS3MJWQD5CSW2GWPRADBXEZINA",
                ConcurrencyStamp = "c8554266-b401-4519-9aeb-a9283053fc58"
            }, new User
            {
                Id = LEGIS_CHAIR_ID,
                UserName = "chair@email.com",
                NormalizedUserName = "chair@EMAIL.COM",
                Email = "chair@email.com",
                NormalizedEmail = "CHAIR@EMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAEAACcQAAAAEALfXOO0MYDpnaWi+2TO6u67hE3xzrew03QVb8Vb3wTOdiKZzWGSm42SscHBRPRT0g==", // Password1!
                SecurityStamp = "VVPCRDAS3MJWQD5CSW2GWPRADBXEZINA",
                ConcurrencyStamp = "c8554266-b401-4519-9aeb-a9283053fc58"
            }, new User
            {
                Id = LEGIS_MEM_ID,
                UserName = "legislator@email.com",
                NormalizedUserName = "LEGISLATOR@EMAIL.COM",
                Email = "legislator@email.com",
                NormalizedEmail = "LEGISLATOR@EMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAEAACcQAAAAEALfXOO0MYDpnaWi+2TO6u67hE3xzrew03QVb8Vb3wTOdiKZzWGSm42SscHBRPRT0g==", // Password1!
                SecurityStamp = "VVPCRDAS3MJWQD5CSW2GWPRADBXEZINA",
                ConcurrencyStamp = "c8554266-b401-4519-9aeb-a9283053fc58"
            }, new User
            {
                Id = HOUSE_MEM_ID,
                UserName = "housemember@email.com",
                NormalizedUserName = "HOUSEMEMBER@EMAIL.COM",
                Email = "housemember@email.com",
                NormalizedEmail = "HOUSEMEMBER@EMAIL.COM",
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
            }, new IdentityUserRole<string>
            {
                RoleId = PROP_MAN_ROLE_ID,
                UserId = PROP_MAN_ID
            }, new IdentityUserRole<string>
            {
                RoleId = EXEC_ROLE_ID,
                UserId = EXEC_ID
            }, new IdentityUserRole<string>
            {
                RoleId = LEGIS_CHAIR_ROLE_ID,
                UserId = LEGIS_CHAIR_ID
            }, new IdentityUserRole<string>
            {
                RoleId = LEGIS_MEM_ROLE_ID,
                UserId = LEGIS_MEM_ID
            }, new IdentityUserRole<string>
            {
                RoleId = HOUSE_MEM_ROLE_ID,
                UserId = HOUSE_MEM_ID
            });
        }
    }
}