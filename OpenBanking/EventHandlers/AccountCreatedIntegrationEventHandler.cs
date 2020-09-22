using Account.IntegrationEvents;
using EventBus.Abstractions;
using Microsoft.Extensions.Logging;
using OpenBanking.Data;
using System;
using System.Threading.Tasks;

namespace OpenBanking.EventHandlers
{
    public class AccountCreatedIntegrationEventHandler : IIntegrationEventHandler<AccountCreatedIntegrationEvent>
    {
        private readonly ILogger<AccountCreatedIntegrationEventHandler> logger;
        private readonly OpenBankingDbContext dbContext;

        public AccountCreatedIntegrationEventHandler(ILogger<AccountCreatedIntegrationEventHandler> logger, OpenBankingDbContext dbContext)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task Handle(AccountCreatedIntegrationEvent @event)
        {
            logger.LogInformation($"Processed {@event.GetType().Name}");

            await dbContext.Accounts.AddAsync(new Entities.Account { Id = @event.AccountId, Balance = @event.Balance });

            await dbContext.SaveChangesAsync();
        }
    }
}
