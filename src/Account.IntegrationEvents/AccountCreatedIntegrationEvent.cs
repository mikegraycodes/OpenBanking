using EventBus.Events;
using System;

namespace Account.IntegrationEvents
{
    public class AccountCreatedIntegrationEvent : IntegrationEvent
    {
        public AccountCreatedIntegrationEvent(Guid accountId, int balance)
        {
            AccountId = accountId;
            Balance = balance;
        }

        public Guid AccountId { get; }
        public int Balance { get; }
    }
}
