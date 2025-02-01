using BankingApp.Core.DTOs;
using BankingApp.Core.Entities;
using BankingApp.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BankingApp.Application.Commands
{
    public record AddUserCommand(UserRegisterDto User) : IRequest<UserResponseDto>;

    public class AddUserCommandHandler(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher, // Ensure password is hashed
        ILogger<AddUserCommandHandler> logger
    ) : IRequestHandler<AddUserCommand, UserResponseDto>
    {
        public async Task<UserResponseDto> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            // Hash the user's password before saving
            request.User.password = passwordHasher.HashPassword(request.User.password);

            Guid id = await userRepository.AddUserAsync(request.User);

            if (id == Guid.Empty)
            {
                logger.LogError("Failed to add user: {Username}", request.User.username);
                throw new ApplicationException("Unable to add new user.");
            }

            var user = await userRepository.GetUsersByIdAsync(id);

            return new UserResponseDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email
            };
        }
    }
}
