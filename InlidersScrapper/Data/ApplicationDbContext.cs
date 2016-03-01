using System.Data.Entity;
using InlidersScrapper.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace InlidersScrapper.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
           : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer<ApplicationDbContext>(new CreateDatabaseIfNotExists<ApplicationDbContext>());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<InlidersScrapper.Models.WebScrapper> WebScrappers { get; set; }
    }
}