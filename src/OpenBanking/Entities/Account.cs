using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OpenBanking.Entities
{
    public class Account
    {

        public Account()
        {

        }

        public Guid Id { get; set; }

        public int Balance { get; set; }

        public IList<Transaction> Transactions { get; private set; } = new List<Transaction>();

    }
}
