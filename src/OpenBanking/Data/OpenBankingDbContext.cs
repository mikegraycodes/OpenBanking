using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace OpenBanking.Data
{
    public class OpenBankingDbContext : DbContext
    {
        public OpenBankingDbContext([NotNull] DbContextOptions options) : base(options)
        {
        }

        public DbSet<Entities.Account> Accounts { get; set; }

        public DbSet<Entities.Transaction> Transactions { get; set; }
    }
}
