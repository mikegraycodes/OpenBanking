using Account.Data;
using Account.Entities;
using Account.IntegrationEvents;
using Account.Models;
using EventBus.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace Account.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly ILogger<AccountsController> logger;
        private readonly IEventBus eventBus;
        private readonly AccountsDbContext dbContext;

        public AccountsController(ILogger<AccountsController> logger, IEventBus eventBus, AccountsDbContext dbContext)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        [HttpGet]
        public async Task<IEnumerable<Entities.Account>> Get(CancellationToken cancellationToken)
        {
            var accounts = await dbContext.Accounts.Include(x => x.Payments).ToListAsync();

            await dbContext.SaveChangesAsync(cancellationToken);

            return accounts;
        }

        [HttpPost]
        public async Task<Entities.Account> Post(CancellationToken cancellationToken)
        {
            var account = new Entities.Account { Balance = 100 };

            await dbContext.AddAsync(account, cancellationToken);

            await dbContext.SaveChangesAsync(cancellationToken);

            //********** YOU SHOULD NEVER PUBLISH EVENTS LIKE THIS **************//

            eventBus.Publish(new AccountCreatedIntegrationEvent(account.Id, account.Balance));

            //********** YOU SHOULD NEVER PUBLISH EVENTS LIKE THIS **************//

            return account;
        }

        [HttpGet("{accountId}")]
        public async Task<Entities.Account> GetAccountById(Guid accountId, CancellationToken cancellationToken)
        {
            var account = await dbContext.Accounts.Include(x => x.Payments).FirstOrDefaultAsync(x => x.Id == accountId);

            await dbContext.SaveChangesAsync(cancellationToken);

            return account;
        }

        [HttpPost("{accountId}/payments")]
        public async Task<Payment> PostPayment(Guid accountId, [FromBody] PaymentPostModel paymentPostModel, CancellationToken cancellationToken)
        {
            var account = await dbContext.Accounts.Include(x => x.Payments).FirstOrDefaultAsync(x => x.Id == accountId);

            var payment = new Payment { Amount = paymentPostModel.Amount, DateTime = DateTime.UtcNow };

            account.Balance += paymentPostModel.Amount;

            account.Payments.Add(payment);

            dbContext.SaveChanges();

            //********** YOU SHOULD NEVER PUBLISH EVENTS LIKE THIS **************//

            eventBus.Publish(new AccountBalanceUpdatedIntegrationEvent(account.Id, account.Balance));
            eventBus.Publish(new PaymentCompletedIntegrationEvent(accountId, payment.Amount, payment.DateTime));

            //********** YOU SHOULD NEVER PUBLISH EVENTS LIKE THIS **************//

            return payment;
        }
    }
}
