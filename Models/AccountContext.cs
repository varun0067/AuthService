using Microsoft.EntityFrameworkCore;

namespace AuthorizationMS.Models
{
    public class AccountContext:DbContext
    {
        public AccountContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Account> Accounts { get; set; }
    }
}
