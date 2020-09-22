using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Account.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Account.Controllers
{
    public class PaymentsController : ControllerBase
    {
        private readonly ILogger<PaymentsController> logger;

        public PaymentsController(ILogger<PaymentsController> logger, AccountsDbContext dbContext)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        //[HttpGet]
        //public async Task<IEnumerable<Entities.Account>> Get(CancellationToken cancellationToken)
        //{
        //    var accounts = await dbContext.Accounts.ToListAsync();

        //    await dbContext.SaveChangesAsync(cancellationToken);

        //    return accounts;
        //}
    }
}
