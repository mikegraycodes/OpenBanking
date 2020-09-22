using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OpenBanking.Data;
using OpenBanking.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace OpenBanking.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OpenBankingP2PController : ControllerBase
    {
        private readonly ILogger<OpenBankingController> logger;
        private readonly OpenBankingDbContext dbContext;
        private readonly IHttpClientFactory factory;

        public OpenBankingP2PController(ILogger<OpenBankingController> logger, OpenBankingDbContext dbContext, IHttpClientFactory factory)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        [HttpGet("Accounts/{accountId}")]
        public async Task<Entities.Account> Get(Guid accountId)
        {
            var client = factory.CreateClient("P2P");

            var response = await client.GetAsync($"http://account/accounts/{accountId}");

            var content = await response.Content.ReadAsStringAsync();

            var account = JsonConvert.DeserializeObject<Models.Account>(content);

            var ret = new Entities.Account { Balance = account.Balance, Id = account.Id };

            foreach(var payment in account.Payments)
            {
                ret.Transactions.Add(new Transaction { Amount = payment.Amount, DateTime = payment.DateTime });
            }


            return ret;
        }
    }
}
