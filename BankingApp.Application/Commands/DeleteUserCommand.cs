﻿using BankingApp.Core.Entities;
using BankingApp.Core.Interfaces;
using MediatR;

namespace BankingApp.Application.Commands
{
    public record DeleteUserCommand(Guid userId) : IRequest<bool>;
   

    public class DeleteUserCommandHandler(IUserRepository userRepository) 
        : IRequestHandler<DeleteUserCommand, bool>
    {
        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            UserEntity user = await userRepository.GetUsersByIdAsync(request.userId);


            if (user == null)
            {
                
                throw new KeyNotFoundException($"User with ID {request.userId} does not exist.");
            }


            var result = await userRepository.DeleteUserAsync(user);

            return result;




        }
    }


}
