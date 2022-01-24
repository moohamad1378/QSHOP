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
        public DbSet<CatalogItem> CatalogItems { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Domain.Catalogs.System> Systems{ get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            SeedData(builder);

        }
        private void SeedData(ModelBuilder modelBuilder)
        {
            var hasher = new PasswordHasher<IdentityUser>();
            modelBuilder.Entity<User>().HasData(new IdentityUser {Id= "8e445865-a24d-4543-a6c6-9443d048cdb9", UserName = "Admin", NormalizedUserName = "ADMIN", NormalizedEmail = "QASEMIYAN.MOSTAFA@GMAIL.COM", Email = "Qasemiyan.mostafa@gmail.com", PasswordHash = hasher.HashPassword( null,"Admin") });
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string> { RoleId = "4", UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9" });

            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "4", Name = nameof(RolesOfUser.Admin) });
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "5", Name = nameof(RolesOfUser.Operator) });
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "6", Name = nameof(RolesOfUser.Customer) });
        }
    }
}
