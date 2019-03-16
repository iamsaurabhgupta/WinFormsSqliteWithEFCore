using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsSqliteWithEFCore.Model
{
    public class UserContext : DbContext
    {
        public DbSet<UserDetails> UserDetails { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=Db\UserDb.sqlite");
        }
    }
}
