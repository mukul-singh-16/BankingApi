using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.Core.Entities
{
    public class UserEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }


        public UserEntity(Guid Id , string Name , decimal Balance) {
            this.Id = Id;
            this.Name = Name;   
            this.Balance = Balance; 

        }
        
        
    }
}
