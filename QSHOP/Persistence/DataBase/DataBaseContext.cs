using Common.UserRoles;
using Domain.Catalogs;
using Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.DataBase
{
    public class DataBaseContext: IdentityDbContext<User, IdentityRole,string>
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options):base(options)
        {

        }
        public DbSet<CatalogType> CatalogTypes { get; set; }
        public DbSet<CatalogBrand> CatalogBrands { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            SeedData(builder);

        }
        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new IdentityUser {Id="abcd1-abcd2-abcd3-abcd4", UserName = "Admin", NormalizedUserName = "ADMIN", NormalizedEmail = "QASEMIYAN.MOSTAFA@GMAIL.COM", Email = "Qasemiyan.mostafa@gmail.com", PasswordHash = "Admin" });
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string> { RoleId = "4", UserId = "abcd1-abcd2-abcd3-abcd4" });

            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "4", Name = nameof(RolesOfUser.Admin) });
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "5", Name = nameof(RolesOfUser.Operator) });
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "6", Name = nameof(RolesOfUser.Customer) });
        }
    }
}
