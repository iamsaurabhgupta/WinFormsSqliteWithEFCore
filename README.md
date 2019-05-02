# WinFormsSqliteWithEFCore
1. Create winforms project
2. Install Package Microsoft.EntityFrameworkCore.Sqlite
3. Install Package Microsoft.EntityFrameworkCore.Design 
4. Install Package Microsoft.EntityFrameworkCore.Tools
5. Create Any Class and inherited DbContext Base Class. For Example  class name is UserContext so 
  public class UserContext: DbContext
  {
      public DbSet<UserDetails> UserDetails { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=Db\UserDb.sqlite");
        }
  }
