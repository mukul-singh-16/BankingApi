using BankingApp.Application.DTOs;
using BankingApp.Core.DTOs;
using BankingApp.Core.Entities;

namespace BankingApp.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserEntity>> GetUsers( PaginationDtos req);
        Task<UserEntity> GetUsersByIdAsync(Guid id);
        Task<UserEntity> GetUserByUsernameAsync(string username);
        Task<Guid> AddUserAsync(UserRegisterDto req);
        
        Task<UserEntity> UpdateUserAsync(UserEntity user , UserUpdateDto updatedUser);
        Task<bool> DeleteUserAsync(UserEntity user);

    }
}
