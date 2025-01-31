namespace BankingApp.Core.Entities
{
    public class UserEntity
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public decimal Balance { get; set; }


        public UserEntity(Guid id, string username, string password,string email , decimal balance)
        {
            Id = id;
            Username = username;
            Email = email;
            Balance = balance;
            Password = password;
        }


    }
}
