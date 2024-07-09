using Inventory.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
                
        }

        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Company> Companies { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Company>().HasData(
                new Company()
                {
                    Id = 1,
                    Name = "ABC Inc.",
                    Address = "Las Piñas City",
                    TelephoneNo = "12345678",
                    MobileNo = "09291234567",
                    ContactPerson = "John Doe",
                    Email = "JohnDoe@gmail.com",
                    SystemOwner = true,
                }
                );
        }
    }
}
