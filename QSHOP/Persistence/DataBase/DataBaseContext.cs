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
    }
}
