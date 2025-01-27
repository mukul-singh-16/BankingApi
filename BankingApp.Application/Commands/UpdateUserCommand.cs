using BankingApp.Core.DTOs;
using BankingApp.Core.Entities;
using BankingApp.Core.Interfaces;
using MediatR;

namespace BankingApp.Application.Commands
{
    public  record UpdateUserCommand(Guid userId , UserRequestDtos updatedUser) :IRequest<UserEntity>;


    public class UpdateUserCommandHandler(IUserRepository userRepository, IPublisher mediator) : IRequestHandler<UpdateUserCommand, UserEntity>
    {
        

        public async Task<UserEntity> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {


            UserEntity user = await userRepository.GetUsersByIdAsync(request.userId);
            if (user == null)
            {

                throw new ArgumentException(message: "not a valid userId");

            }

            return await userRepository.UpdateUserAsync(user , request.updatedUser);
            //await mediator.Publish(new UserCreatedEvent(user.Id));
            //return user;
        }
    }
}
