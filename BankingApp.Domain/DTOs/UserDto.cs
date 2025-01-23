using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.Core.DTOs
{
    public class UserDto
    {
        public string Name { get; set; }


        public UserDto(string Name)
        {
            this.Name = Name;
        }
    }
}
