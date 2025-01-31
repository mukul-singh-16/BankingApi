using BankingApp.Core.DTOs;
using BankingApp.Core.Entities;
using BankingApp.Core.Interfaces;
using MediatR;

namespace BankingApp.Application.Commands
{
    public  record AddUserCommand(UserRegisterDto User):IRequest<UserEntity>;


    public class AddUserCommandHandler(IUserRepository userRepository , IPublisher mediator) : IRequestHandler<AddUserCommand, UserEntity>
    {
        public async Task<UserEntity > Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            Guid id= await userRepository.AddUserAsync(request.User);
            if (id == Guid.Empty)
            {
                throw new ArgumentException(message: "unable to add new user");
            }

            return await userRepository.GetUsersByIdAsync(id);
            
        }
    }
}
