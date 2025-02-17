using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Blog.Models;

namespace Code1stUserRoles.Data
{
    public class SeedUsersRoles
    {
        private readonly List<CustomRole> _roles;
        private readonly List<CustomUser> _users;
        private readonly List<IdentityUserRole<string>> _userRoles; 

        public SeedUsersRoles()
        {
            _roles = GetRoles();
            _users = GetUsers();
            _userRoles = GetUserRoles(_users, _roles);
        } 

        public List<CustomRole> Roles { get { return _roles; } }
        public List<CustomUser> Users { get { return _users; } }
        public List<IdentityUserRole<string>> UserRoles { get { return _userRoles; } }

        private List<CustomRole> GetRoles()
        {
            // Seed Roles
            var adminRole = new CustomRole("Admin")
            {
                Id = Guid.NewGuid().ToString(),
                NormalizedName = "ADMIN",
                Description = "Super User",
                CreatedDate = DateTime.UtcNow
            };

            var contributorRole = new CustomRole("Contributor")
            {
                Id = Guid.NewGuid().ToString(),
                NormalizedName = "CONTRIBUTOR",
                Description = "Regular User",
                CreatedDate = DateTime.UtcNow
            };

            List<CustomRole> roles = new List<CustomRole>()
            {
                adminRole,
                contributorRole
            };
            return roles;
        }

        private List<CustomUser> GetUsers()
        {
            string pwd = "P@$$w0rd"; 
            var passwordHasher = new PasswordHasher<CustomUser>();

            // Seed Users
            var adminUser = new CustomUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "a@a.a",
                Email = "a@a.a",
                EmailConfirmed = true,
                FirstName = "Admin",
                LastName = "Lee",
                IsApproved = true,
                NormalizedUserName = "A@A.A",
                NormalizedEmail = "A@A.A"
            };
            adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, pwd);

            var contributorUser1 = new CustomUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "c@c.c",
                Email = "c@c.c",
                EmailConfirmed = true,
                FirstName = "Contributor",
                LastName = "Lee",
                IsApproved = true,
                NormalizedUserName = "C@C.C",
                NormalizedEmail = "C@C.C"
            };
            contributorUser1.PasswordHash = passwordHasher.HashPassword(contributorUser1, pwd);

            var contributorUser2 = new CustomUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "d@d.d",
                Email = "d@d.d",
                EmailConfirmed = true,
                FirstName = "Contributor",
                LastName = "Lee",
                IsApproved = true,
                NormalizedUserName = "D@D.D",
                NormalizedEmail = "D@D.D"
            };
            contributorUser2.PasswordHash = passwordHasher.HashPassword(contributorUser2, pwd);

            List<CustomUser> users = new List<CustomUser>()
            {
                adminUser,
                contributorUser1,
                contributorUser2,
            };
            return users;
        }

        private List<IdentityUserRole<string>> GetUserRoles(List<CustomUser> users, List<CustomRole> roles)
        {
            // Seed UserRoles
            List<IdentityUserRole<string>> userRoles = new List<IdentityUserRole<string>>();

            userRoles.Add(new IdentityUserRole<string>
            {
                UserId = users[0].Id,
                RoleId = roles.First(q => q.Name == "Admin").Id
            });
            userRoles.Add(new IdentityUserRole<string> {
                UserId = users[1].Id,
                RoleId = roles.First(q => q.Name == "Contributor").Id
            });

            userRoles.Add(new IdentityUserRole<string> {
                UserId = users[2].Id,
                RoleId = roles.First(q => q.Name == "Contributor").Id
            });

            return userRoles;
        }
    }
}
