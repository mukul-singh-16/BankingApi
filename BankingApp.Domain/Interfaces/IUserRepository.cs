using BankingApp.Application.DTOs;
using BankingApp.Core.DTOs;
using BankingApp.Core.Entities;

namespace BankingApp.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserEntity>> GetUsers( PaginationDtos req);
        Task<UserEntity> GetUsersByIdAsync(Guid id);
        Task<UserEntity> AddUserAsync(UserRequestDtos req);
        Task<UserEntity> UpdateUserAsync(UserEntity user , UserRequestDtos updatedUser);
        Task<bool> DeleteUserAsync(UserEntity user);
        //Task<UserEntity> UpdateUserAsync(UserEntity user, object updatedUser);
    }
}
