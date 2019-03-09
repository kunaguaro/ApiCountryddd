

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApi.Domain.Entities;
using WebApi.Infrastructure.Data.EntityDbMapping;

namespace WebApi.Infrastructure.Data.Context
{
    public class SqlServerContext :   IdentityDbContext<ApplicationUser>
    {
        public DbSet<Country> Country { get; set; }

        public SqlServerContext(DbContextOptions<SqlServerContext> options) : base(options)
        {
            
        }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Country>(new CountryMap().Configure);
            // ModelBuilderExtensions.Seed(modelBuilder);

        }



    }

    //Data for first time on table
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>().HasData(
                new Country
                {
                    Id = 1,
                    Name = "Venezuela",
                    Population = 300000000,
                    Area = 230103
                   
                },
                new Country
                {
                    Id = 2,
                    Name = "Peru",
                    Population = 260000000,
                    Area =33249
                 
                }

            );


        }
    }
}
