using EventBus.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Account.IntegrationEvents
{
    public class AccountBalanceUpdatedIntegrationEvent : IntegrationEvent
    {
        public AccountBalanceUpdatedIntegrationEvent(Guid accountId, int balance)
        {
            AccountId = accountId;
            Balance = balance;
        }

        public Guid AccountId { get; }
        public int Balance { get; }
    }
}
