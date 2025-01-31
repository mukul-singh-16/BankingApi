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



        public async Task<UserEntity> GetUserByUsernameAsync(string username)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Username == username);

            return user;
        }





        public async Task<Guid> AddUserAsync(UserRegisterDto NewUser)
        {

            Guid userid = Guid.NewGuid();
            UserEntity newUser = new UserEntity(userid, NewUser.username, NewUser.password,NewUser.emailAddress,0);
            _dbContext.Users.Add(newUser);
            await _dbContext.SaveChangesAsync();

            return userid;
        }


        

        public async Task<UserEntity> UpdateUserAsync(UserEntity user, UserUpdateDto UpdatedUser)
        {
            user.Password = UpdatedUser.password;
            user.Email = UpdatedUser.email;

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
