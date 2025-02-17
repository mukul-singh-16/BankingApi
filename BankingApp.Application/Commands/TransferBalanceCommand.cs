﻿using System.ComponentModel;
using BankingApp.Application.Queries;
using BankingApp.Core.DTOs;
using BankingApp.Core.Entities;
using BankingApp.Core.Interfaces;
using MediatR;

namespace BankingApp.Application.Commands
{
    public  record TransferBalanceCommand(Guid FromUserId ,TransferBalanceDtos Dto):IRequest<bool>;


    public class TransferBalanceCommandHandler(ITransactionRepository transactionRepository , IUserRepository userRepository ) : IRequestHandler< TransferBalanceCommand, bool>
    {
        public async Task<bool > Handle(TransferBalanceCommand request, CancellationToken cancellationToken)
        {
    
            if (request.FromUserId == Guid.Empty || request.Dto.ToUserId == Guid.Empty || request.Dto.Amount <= 0)
            {
                throw new ArgumentException("not a valid input");
            }

            

            UserEntity fromUser = await userRepository.GetUsersByIdAsync(request.FromUserId);
            if (fromUser == null)
                throw new ArgumentException("Invalid From User ID");

            UserEntity toUser = await userRepository.GetUsersByIdAsync(request.Dto.ToUserId);


            if(fromUser.Balance < request.Dto.Amount)
                throw new ArgumentException("Insufficient balance");



            
            if (toUser == null)
                throw new ArgumentException("Invalid To User ID");




            return await transactionRepository.TransferBalanceAsync(fromUser, toUser , request.Dto.Amount );

            
        }

        
    }
}
