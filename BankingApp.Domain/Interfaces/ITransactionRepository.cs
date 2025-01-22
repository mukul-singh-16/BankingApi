using BankingApp.Core.Entities;

namespace BankingApp.Core.Interfaces
{
    public interface ITransactionRepository
    {
        Task<bool> TransferBalanceAsync(Guid fromUserId, Guid toUserId, decimal amount);
        Task<bool> AddBalanceAsync(Guid userId, decimal amount);
        Task<bool> RemoveBalanceAsync(Guid userId, decimal amount);
        //Task<IEnumerable<TransactionEntity>> GetTransactionHistoryAsync(Guid userId, int page);
        //Task GetTransactionHistoryAsync(Guid userId, int page, int pageSize);
        Task<IEnumerable<TransactionEntity>> GetTransactionHistoryAsync(Guid userId, int page, int pageSize);
        
    }
}
