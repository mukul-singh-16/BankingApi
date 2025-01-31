using BankingApp.Core.Entities;

namespace BankingApp.Core.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(UserEntity user);
    }
}
