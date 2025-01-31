using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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


    public class LoginDto
    {
        public string Username { get; set; }
        public string Password { get; set; }


        public LoginDto(string Username , string Password)
        {
            this.Username = Username;
            this.Password = Password;   
            
        }
    }

    public class UserUpdateDto
    {
        public string password { get; set; }
        public string email { get; set; }   
    }


    public class UserRegisterDto
    {
        public string username { get; set; }
        public string password { get; set; }
        public string emailAddress { get; set; }
    }



}
