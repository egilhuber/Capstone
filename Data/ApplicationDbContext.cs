using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using healthicly.Models;

namespace healthicly.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CleaningTask> CleaningTasks { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<InHouseActivity> InHouseActivities { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<Outing> Outings { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<ViewingParty> ViewingParties { get; set; }
        public DbSet<WishListItem> WishListItems { get; set; }
        public DbSet<Alert> Alerts { get; set; }

    }
}
