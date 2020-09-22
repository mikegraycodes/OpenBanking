using Account.IntegrationEvents;
using EventBus.Abstractions;
using Microsoft.EntityFrameworkCore;
using OpenBanking.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace OpenBanking.EventHandlers
{
    public class AccountBalanceUpdatedIntegrationEventHandler : IIntegrationEventHandler<AccountBalanceUpdatedIntegrationEvent>
    {
        private readonly OpenBankingDbContext dbContext;

        public AccountBalanceUpdatedIntegrationEventHandler(OpenBankingDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task Handle(AccountBalanceUpdatedIntegrationEvent @event)
        {
            var account = await dbContext.Accounts.FirstOrDefaultAsync(x => x.Id == @event.AccountId);

            account.Balance = @event.Balance;

            await dbContext.SaveChangesAsync();
        }
    }
}
