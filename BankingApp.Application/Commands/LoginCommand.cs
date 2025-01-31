using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BankingApp.Core.DTOs;
using BankingApp.Core.Interfaces;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace BankingApp.Application.Commands
{
    public record LoginCommand(LoginDto loginDto) : IRequest<string>;

    public class LoginUserCommandHandler(IUserRepository _userRepository 
        , IJwtService _jwtService
        ) : IRequestHandler<LoginCommand, string>
    {


        public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            
            var user = await _userRepository.GetUserByUsernameAsync(request.loginDto.Username);
            if (user == null || user.Password != request.loginDto.Password) // Password should ideally be hashed and verified
            {
                throw new Exception("Invalid credentials");
            }

            // Generate JWT token
            return _jwtService.GenerateToken(user);

            //return "login sucessfull";
        }
    }
}
