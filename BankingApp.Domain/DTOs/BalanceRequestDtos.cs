using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.Core.DTOs
{
    public class BalanceRequestDtos
    {
        public Guid UserId { get; set; }
        public decimal Amount { get; set; }
    }
}
