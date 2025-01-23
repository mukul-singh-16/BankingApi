using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingApp.Core.DTOs;
using BankingApp.Core.Entities;

namespace BankingApp.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserDto>> GetUsers();
        Task<UserEntity> GetUsersByIdAsync(Guid id);
        Task<UserEntity> AddUserAsync(UserRequest newUser);
        Task<UserEntity> UpdateUserAsync(Guid userId, UserRequest updatedUser);
        Task<bool> DeleteUserAsync(Guid userId);

        



    }
}
