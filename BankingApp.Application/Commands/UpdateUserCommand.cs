using BankingApp.Core.DTOs;
using BankingApp.Core.Entities;
using BankingApp.Core.Interfaces;
using MediatR;

namespace BankingApp.Application.Commands
{
    public  record UpdateUserCommand(Guid userId , UserUpdateDto updatedUser) :IRequest<UserResponseDto>;


    public class UpdateUserCommandHandler(IUserRepository userRepository, IPublisher mediator) : IRequestHandler<UpdateUserCommand, UserResponseDto>
    {
        

        public async Task<UserResponseDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {

            UserEntity user = await userRepository.GetUsersByIdAsync(request.userId);
            

            user =await userRepository.UpdateUserAsync(user , request.updatedUser);

            UserResponseDto userResponseDto = new UserResponseDto{
                Id=user.Id,
                Email=user.Email,
                Username= user.Username
            };

            return userResponseDto;
        }
    }
}
