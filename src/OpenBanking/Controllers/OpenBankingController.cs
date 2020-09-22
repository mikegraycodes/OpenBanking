using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OpenBanking.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace OpenBanking.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OpenBankingController : ControllerBase
    {
        private readonly ILogger<OpenBankingController> logger;
        private readonly OpenBankingDbContext dbContext;

        public OpenBankingController(ILogger<OpenBankingController> logger, OpenBankingDbContext dbContext)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        [HttpGet("Accounts/{accountId}")]
        public async Task<Entities.Account> Get(Guid accountId)
        {
            var account = await dbContext.Accounts.Include(x => x.Transactions).FirstOrDefaultAsync(x => x.Id == accountId);

            return account;
        }
    }
}
