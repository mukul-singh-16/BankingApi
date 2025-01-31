using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using BankingApp.Application.DTOs;
using BankingApp.Core.DTOs;
using BankingApp.Core.Entities;
using BankingApp.Core.Interfaces;
using MediatR;

namespace BankingApp.Application.Queries
{
    public record GetAllUserQuery(PaginationDtos pg) : IRequest<IEnumerable<UserDto>>;


    internal class GetAllUserQueryHandler(IUserRepository userRepository)
        :IRequestHandler<GetAllUserQuery,IEnumerable<UserDto>>

    {
        public async Task<IEnumerable<UserDto>> Handle(GetAllUserQuery request , CancellationToken cancellationToken)
        {

            var allUsers = await userRepository.GetUsers(request.pg);

            if (allUsers == null || !allUsers.Any())
            {
                return Enumerable.Empty<UserDto>();
            }

            
            var userDtos = allUsers.Select(user =>
            {
                return new UserDto(user.Id, user.Username);
                
            });

            return userDtos;
        }

    }

}
