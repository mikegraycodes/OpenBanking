using Account.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Account.Data
{
    public class AccountsDbContext : DbContext
    {
        public AccountsDbContext([NotNull] DbContextOptions options) : base(options)
        {
        }

        public DbSet<Entities.Account> Accounts { get; set; }

        public DbSet<Payment> Payments { get; set; }
    }
}
