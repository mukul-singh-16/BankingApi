using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.Core.DTOs
{
    public class TransferBalanceDtos
    {
        public Guid FromUserId { get; set; }
        public Guid ToUserId { get; set; }
        public decimal Amount { get; set; }


    }
}
