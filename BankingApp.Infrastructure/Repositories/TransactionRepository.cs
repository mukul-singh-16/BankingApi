using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankingApp.Core.Entities;
using BankingApp.Core.Interfaces;
using BankingApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly BankingAppContext _dbContext;

        public TransactionRepository(BankingAppContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> TransferBalanceAsync(Guid fromUserId, Guid toUserId, decimal amount)
        {
            if (amount <= 0) return false;

            var fromUser = await _dbContext.Users.FindAsync(fromUserId);
            var toUser = await _dbContext.Users.FindAsync(toUserId);

            if (fromUser == null || toUser == null || fromUser.Balance < amount) return false;

            try
            {
                using var transaction = await _dbContext.Database.BeginTransactionAsync();

                fromUser.Balance -= amount;
                toUser.Balance += amount;

                var transactionLog = new TransactionEntity
                {
                    Id = Guid.NewGuid(),
                    FromUserId = fromUserId,  
                    ToUserId =  toUserId,    
                    Amount = amount,
                    Type = "Transfer",
                    Date = DateTime.UtcNow
                };

                _dbContext.Transactions.Add(transactionLog);
                await _dbContext.SaveChangesAsync();

                
                await transaction.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Error adding balance: {ex.Message}");
                return false;
            }
        }

        
        public async Task<bool> AddBalanceAsync(Guid userId, decimal amount)
        {
            if (amount <= 0) return false;

            var user = await _dbContext.Users.FindAsync(userId);
            if (user == null) return false;

            try
            {
                using var transaction = await _dbContext.Database.BeginTransactionAsync();

                user.Balance += amount;

                var transactionLog = new TransactionEntity
                {
                    Id = Guid.NewGuid(),
                    FromUserId = userId,  
                    ToUserId = userId,    
                    Amount = amount,
                    Type = "Deposit",
                    Date = DateTime.UtcNow
                };

                _dbContext.Transactions.Add(transactionLog);
                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding balance: {ex.Message}");
                return false;
            }
        }

        
        public async Task<bool> RemoveBalanceAsync(Guid userId, decimal amount)
        {
            if (amount <= 0) return false;

            var user = await _dbContext.Users.FindAsync(userId);
            if (user == null || user.Balance < amount) return false;

            try
            {
                // Start a transaction for atomic operation
                using var transaction = await _dbContext.Database.BeginTransactionAsync();

                Console.WriteLine("remove balance start");

                // Update balance
                user.Balance -= amount;

                // Log the withdrawal transaction
                var transactionLog = new TransactionEntity
                {
                    Id = Guid.NewGuid(),
                    FromUserId = userId,
                    ToUserId = userId, 
                    Amount = amount,
                    Type = "Withdrawal",
                    Date = DateTime.UtcNow
                };

                _dbContext.Transactions.Add(transactionLog);
                await _dbContext.SaveChangesAsync();

                // Commit transaction
                await transaction.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Log exception if needed (you can use a logging framework here)
                Console.WriteLine($"Error removing balance: {ex.Message}");
                return false;
            }
        }

        // Method to get transaction history for a specific user
        public async Task<IEnumerable<TransactionEntity>> GetTransactionHistoryAsync(Guid userId, int page, int pageSize)
        {
            return await _dbContext.Transactions
                .Where(t => t.FromUserId == userId || t.ToUserId == userId)
                .OrderByDescending(t => t.Date)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        

        
    }
}
