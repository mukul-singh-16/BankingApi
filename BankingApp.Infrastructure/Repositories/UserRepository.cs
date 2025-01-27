using BankingApp.Application.DTOs;
using BankingApp.Core.DTOs;
using BankingApp.Core.Entities;
using BankingApp.Core.Interfaces;
using BankingApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.Infrastructure.Repositories
{
    public class UserRepository(AppDbContext _dbContext ) : IUserRepository  {
        

        public async Task<IEnumerable<UserEntity>> GetUsers( PaginationDtos pagination)
        {
            return await _dbContext.Users
            .Skip((pagination.page - 1) * pagination.pageSize)
            .Take(pagination.pageSize)
            .ToListAsync();
        }



        public async Task<UserEntity> GetUsersByIdAsync(Guid id)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        }




        public async Task<UserEntity> AddUserAsync(UserRequestDtos req)
        {

            UserEntity newUser = new UserEntity(Guid.NewGuid(),req.username,req.password,0);
            _dbContext.Users.Add(newUser);
            await _dbContext.SaveChangesAsync();

            return await GetUsersByIdAsync(newUser.Id);
        }


        

        public async Task<UserEntity> UpdateUserAsync(UserEntity user, UserRequestDtos req)
        {
            user.Username = req.username;
            await _dbContext.SaveChangesAsync();
            return user;
        }



        public async Task<bool> DeleteUserAsync(UserEntity user)
        {
            _dbContext.Users.Remove(user);
            return await _dbContext.SaveChangesAsync() > 0;
        }
       
    }
}
