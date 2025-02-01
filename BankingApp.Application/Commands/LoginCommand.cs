using System.Threading;
using System.Threading.Tasks;
using BankingApp.Core.DTOs;
using BankingApp.Core.Entities;
using BankingApp.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BankingApp.Application.Commands
{
    public record LoginCommand(LoginDto loginDto) : IRequest<LoginResponseDto>;

    public class LoginUserCommandHandler(
        IUserRepository _userRepository,
        IJwtService _jwtService,
        IPasswordHasher _passwordHasher, 
        ILogger<LoginUserCommandHandler> _logger 
    ) : IRequestHandler<LoginCommand, LoginResponseDto>
    {
        public async Task<LoginResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            UserEntity user = await _userRepository.GetUserByUsernameAsync(request.loginDto.Username);

            if (user == null || !_passwordHasher.VerifyPassword(request.loginDto.Password, user.Password))
            {
                _logger.LogWarning("Login failed for user: {Username}", request.loginDto.Username);
                throw new UnauthorizedAccessException("Invalid username or password.");
            }

            // Generate JWT token
            var token = _jwtService.GenerateToken(user);

            _logger.LogInformation("User {Username} logged in successfully.", request.loginDto.Username);

            return new LoginResponseDto
            {
                Token = token,
                Id = user.Id,
                Balance = user.Balance,
                Username = user.Username
            };
        }
    }
}
