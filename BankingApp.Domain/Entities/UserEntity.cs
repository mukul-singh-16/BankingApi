namespace BankingApp.Core.Entities
{
    public class UserEntity
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public decimal Balance { get; set; }
        //public Guid Guid { get; }
        //public int V { get; }

        public UserEntity(Guid id, string username,string password, decimal balance)
        {
            Id = id;
            Username = username;
            Balance = balance;
            Password = password;
        }

       
    }
}
