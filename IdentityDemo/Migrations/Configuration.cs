namespace IdentityDemo.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "IdentityDemo.Models.ApplicationDbContext";
        }

        protected override void Seed(ApplicationDbContext context)
        {
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);
            foreach (string roleName in new[] { "Admin", "Editor" }) {
                if (!context.Roles.Any(r => r.Name == roleName)) {
                    var role = new IdentityRole { Name = roleName };
                    var result = roleManager.Create(role);
                    if (!result.Succeeded) throw new Exception(string.Join("\n", result.Errors));
                }
            }

            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);            
            foreach (string email in new[] { "jocke@lexicon.se", "editor@lexicon.se", "admin@lexicon.se", "root@lexicon.se" })
            {
                if (!context.Users.Any(u => u.UserName == email))
                {
                    var user = new ApplicationUser {
                        UserName = email,
                        Email = email,
                        LockoutEnabled = true
                    };
                    var result = userManager.Create(user, "foobar");
                    if (!result.Succeeded) throw new Exception(string.Join("\n", result.Errors));
                }
            }

            var adminUser = userManager.FindByName("admin@lexicon.se");
            userManager.AddToRole(adminUser.Id, "Admin");

            var editorUser = userManager.FindByName("editor@lexicon.se");
            userManager.AddToRole(editorUser.Id, "Editor");

            var rootUser = userManager.FindByName("root@lexicon.se");
            userManager.AddToRole(rootUser.Id, "Editor");
            userManager.AddToRole(rootUser.Id, "Admin");
        }
    }
}
