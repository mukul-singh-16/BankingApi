using BankingApp.Core.DTOs;
using BankingApp.Core.Entities;
using BankingApp.Core.Interfaces;
using MediatR;

namespace BankingApp.Application.Commands
{
    public  record AddUserCommand(UserRequestDtos User):IRequest<UserEntity>;


    public class AddUserCommandHandler(IUserRepository userRepository , IPublisher mediator) : IRequestHandler<AddUserCommand, UserEntity>
    {
        public async Task<UserEntity > Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            return await userRepository.AddUserAsync(request.User);
            
        }
    }
}
