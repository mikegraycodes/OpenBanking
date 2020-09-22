using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Account.Entities
{
    public class Account
    {
        public Account()
        {

        }

        public Guid Id { get; set; }

        public int Balance { get; set; }

        public IList<Payment> Payments { get; set; } = new List<Payment>();
    }
}
