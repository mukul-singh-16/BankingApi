using BankingApp.Application.DTOs;
using BankingApp.Core.Entities;

namespace BankingApp.Core.Interfaces
{
    public interface ITransactionRepository
    {
        Task<bool> TransferBalanceAsync(UserEntity fromUser, UserEntity toUser, decimal amount);
        Task<bool> AddBalanceAsync(UserEntity user, decimal amount);
        Task<bool> RemoveBalanceAsync(UserEntity user, decimal amount);
        Task<IEnumerable<TransactionEntity>> GetTransactionHistoryAsync(Guid userId, PaginationDtos pagination);
        //Task<bool> AddBalanceAsync(Task<UserEntity> user, decimal amount);
    }
}
