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
    public class PaymentCompletedIntegrationEventHandler : IIntegrationEventHandler<PaymentCompletedIntegrationEvent>
    {
        private readonly OpenBankingDbContext dbContext;

        public PaymentCompletedIntegrationEventHandler(OpenBankingDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }


        public async Task Handle(PaymentCompletedIntegrationEvent @event)
        {
            var account = await dbContext.Accounts.Include(x => x.Transactions).FirstOrDefaultAsync(x => x.Id == @event.AccountId);

            var trans = new Entities.Transaction { Amount = @event.Amount, DateTime = @event.PaymentTime };

            account.Transactions.Add(trans);

            await dbContext.SaveChangesAsync();
        }
    }
}
