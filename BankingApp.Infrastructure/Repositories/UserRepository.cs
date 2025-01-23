﻿using BankingApp.Core.DTOs;
using BankingApp.Core.Entities;
using BankingApp.Core.Interfaces;
using BankingApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository  {
        private readonly BankingAppContext _dbContext;

        public UserRepository(BankingAppContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<UserDto>> GetUsers()
        {
            return await _dbContext.Users
                .Select(user => new UserDto(user.Name))
                .ToListAsync();
        }

        public async Task<UserEntity> GetUsersByIdAsync(Guid id)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<UserEntity> AddUserAsync(UserRequest user)
        {
            UserEntity newUser = new(Guid.NewGuid(),user.Name,0);
         
            _dbContext.Users.Add(newUser);
            await _dbContext.SaveChangesAsync();

            return await GetUsersByIdAsync(newUser.Id);
        }


        

        public async Task<UserEntity> UpdateUserAsync(Guid userId, UserRequest updatedUser)
        {
            var user = await GetUsersByIdAsync(userId);

            if (user == null) 
                return null;

               

            user.Name = updatedUser.Name;


            await _dbContext.SaveChangesAsync();

            return user;
        }

        public async Task<bool> DeleteUserAsync(Guid userId)
        {
            var user = await GetUsersByIdAsync(userId);
            if (user == null) return false;

            _dbContext.Users.Remove(user);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        
    }
}
