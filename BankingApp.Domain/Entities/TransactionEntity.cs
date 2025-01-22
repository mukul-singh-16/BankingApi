using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.Core.Entities
{
    public class TransactionEntity
    {
        public Guid Id { get; set; } 
        public Guid FromUserId { get; set; } 
        public Guid ToUserId { get; set; } 
        public decimal Amount { get; set; } 
        public DateTime Date { get; set; } 
        public string Type { get; set; } // Type of transaction: "Transfer", "Add", "Remove"
    }
}
