using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OpenBanking.Entities
{
    public class Transaction
    {
        public Transaction()
        {

        }
        [JsonIgnore]
        public Guid Id { get; set; }

        public int Amount { get; set; }
        public DateTime DateTime { get; set; }
    }
}
