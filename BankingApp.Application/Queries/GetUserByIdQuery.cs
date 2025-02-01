using BankingApp.Core.DTOs;
using BankingApp.Core.Entities;
using BankingApp.Core.Interfaces;
using MediatR;

namespace BankingApp.Application.Queries
{
    public record GetUserByIdQuery(Guid UserId ) : IRequest<UserProfileDto>;


    internal class GetUserByIdQueryHandler(IUserRepository userRepository)
        : IRequestHandler<GetUserByIdQuery, UserProfileDto>

    {
        public async Task<UserProfileDto> Handle(GetUserByIdQuery request , CancellationToken cancellationToken)
        {

            UserEntity user = await userRepository.GetUsersByIdAsync(request.UserId );

            if (user == null)
            {
                throw new InvalidOperationException("No users are present in the System.");
            }

            UserProfileDto userProfileDto = new UserProfileDto{
                Id = user.Id,
                Username =user.Username,
                Email = user.Email,
                Balance = user.Balance
            };

            return userProfileDto;
        }
        

    }

}
