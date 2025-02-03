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

        // public UserDto()
        // {
        // }
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
        public required string password { get; set; }
        public required string email { get; set; }   
    }


    public class UserRegisterDto
    {
        public required string username { get; set; }
        public required string password { get; set; }
        public required string emailAddress { get; set; }
    }



    public class LoginResponseDto
    {
        public Guid Id {get; set;}
        public required string Token { get; set; }
        public required string Username { get; set; }
        public decimal Balance {get; set;}

    }


    public class UserResponseDto
    {
        public Guid Id { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }        
    }

    public class UserProfileDto
    {
        public Guid Id { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public decimal Balance {get; set;}
    }
}
