using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingApp.Application.DTOs;
using BankingApp.Core.Entities;
using BankingApp.Core.Interfaces;
using MediatR;

namespace BankingApp.Application.Queries
{
    public record class GetTransactionHistoryQuery(Guid userID , PaginationDtos pg) : 
        IRequest<IEnumerable<TransactionEntity>>;

    internal class GetTransactionHistoryQueryHandler(ITransactionRepository TransactionRepository)
        : IRequestHandler<GetTransactionHistoryQuery, IEnumerable<TransactionEntity>>
    {

        public async Task<IEnumerable<TransactionEntity>> Handle(GetTransactionHistoryQuery request, CancellationToken cancellationToken)
        {
            var transactions =  await TransactionRepository.GetTransactionHistoryAsync(request.userID , request.pg);

            

            return transactions;
        }
    }
}
