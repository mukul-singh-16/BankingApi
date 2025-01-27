using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.Core.DTOs
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; }


        public UserDto(Guid id , string username)
        {
            this.Id = id;

            this.Username = username;
        }
    }
}
