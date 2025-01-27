using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingApp.Core.DTOs;
using BankingApp.Core.Entities;
using BankingApp.Core.Interfaces;
using MediatR;

namespace BankingApp.Application.Queries
{
    public record GetUserByIdQuery(Guid UserId ) : IRequest<UserDto>;


    internal class GetUserByIdQueryHandler(IUserRepository userRepository)
        : IRequestHandler<GetUserByIdQuery, UserDto>

    {
        public async Task<UserDto> Handle(GetUserByIdQuery request , CancellationToken cancellationToken)
        {

            var user = await userRepository.GetUsersByIdAsync(request.UserId );

            if (user == null)
            {
                throw new InvalidOperationException("No users are present in the System.");
            }



            

            return new UserDto(user.Id, user.Username);
        }
        

    }

}
