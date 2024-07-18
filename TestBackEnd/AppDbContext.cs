

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection.Emit;
using System.Xml.Linq;
using TestBackEnd.Domain;

namespace TestBackEnd
{
    public class AppDbContext : IdentityDbContext
    {
        private readonly IConfiguration configuration;
        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration)
            : base(options)
        {
            this.configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Category>().HasData(
               new Category
               {
                   Id = 1,
                   Name = "Electronics"
               },
               new Category
               {
                   Id = 3,
                   Name = "Smartphone"

               }, 
               new Category
               {
                   Id = 2,
                   Name = "Laptop"

               });

            builder.Entity<Product>().HasData(
               new Product
               {
                   Id=1,
                   Name = "Iphone",
                   Price = 15,
                   CategoryId = 1
               },
               new Product
               {
                   Id = 2,
                   Name = "Asus",
                   Price = 20,
                   CategoryId = 3
               },
               new Product
               {
                   Id = 3,
                   Name = "Dell",
                   Price = 25,
                   CategoryId = 3
               }

       );
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();

                if (tableName != null && tableName.StartsWith("AspNet"))
                {
                    entityType.SetTableName(tableName.Replace("AspNet", "Test_"));
                }
            }
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this.configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found."));

        }
    }

}