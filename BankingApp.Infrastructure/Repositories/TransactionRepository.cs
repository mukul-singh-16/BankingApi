using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankingApp.Application.DTOs;
using BankingApp.Core.Entities;
using BankingApp.Core.Interfaces;
using BankingApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.Infrastructure.Repositories
{
    public class TransactionRepository(AppDbContext _dbContext) : ITransactionRepository
    {

        public async Task<bool> TransferBalanceAsync(UserEntity fromUser, UserEntity toUser, decimal amount)
        {
            
            try
            {
                using var transaction = await _dbContext.Database.BeginTransactionAsync();

                fromUser.Balance -= amount;
                toUser.Balance += amount;

                var transactionLog = new TransactionEntity
                {
                    Id = Guid.NewGuid(),
                    FromUserId = fromUser.Id,  
                    ToUserId =  toUser.Id,    
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

        
        public async Task<bool> AddBalanceAsync(UserEntity user, decimal amount)
        {

            //try
            //{
                using var transaction = await _dbContext.Database.BeginTransactionAsync();

                user.Balance += amount;

                var transactionLog = new TransactionEntity
                {
                    Id = Guid.NewGuid(),
                    FromUserId = user.Id,  
                    ToUserId = user.Id,    
                    Amount = amount,
                    Type = "Deposit",
                    Date = DateTime.UtcNow
                };

                _dbContext.Transactions.Add(transactionLog);
                await _dbContext.SaveChangesAsync();

                await transaction.CommitAsync();
                return true;
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine($"Error adding balance: {ex.Message}");
            //    return false;
            //}
        }

        
        public async Task<bool> RemoveBalanceAsync(UserEntity user, decimal amount)
        {
            
            try
            {
                
                using var transaction = await _dbContext.Database.BeginTransactionAsync();

                user.Balance -= amount;


                TransactionEntity transactionLog = new TransactionEntity
                {
                    Id = Guid.NewGuid(),
                    FromUserId = user.Id,
                    ToUserId = user.Id,
                    Amount = amount,
                    Type = "Withdrawal",
                    Date = DateTime.UtcNow
                };

                _dbContext.Transactions.Add(transactionLog);
                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Error removing balance: {ex.Message}");
                return false;
            }
        }





        
        public async Task<IEnumerable<TransactionEntity>> GetTransactionHistoryAsync(Guid userId, PaginationDtos pagination)
        {
            return await _dbContext.Transactions
                .Where(t => t.FromUserId == userId || t.ToUserId == userId)
                .OrderByDescending(t => t.Date)
                .Skip((pagination.page - 1) * pagination.pageSize)
                .Take(pagination.pageSize)
                .ToListAsync();
        }

        

        
    }
}
