using EventBus.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Account.IntegrationEvents
{
    public class PaymentCompletedIntegrationEvent : IntegrationEvent
    {
        public PaymentCompletedIntegrationEvent(Guid accountId, int amount, DateTime paymentTime)
        {
            AccountId = accountId;
            Amount = amount;
            PaymentTime = paymentTime;
        }

        public Guid AccountId { get; }
        public int Amount { get; }
        public DateTime PaymentTime { get; }
    }
}
