using BankingApp.Core.Entities;
using BankingApp.Core.Interfaces;
using MediatR;

namespace BankingApp.Application.Commands
{
    public record RemoveBalanceCommand(Guid userId, decimal amount) : IRequest<bool>;


    public class RemoveBalanceCommandHandler(ITransactionRepository transactionRepository, IUserRepository userRepository, IPublisher mediator) : IRequestHandler<RemoveBalanceCommand, bool>
    {
        public async Task<bool> Handle(RemoveBalanceCommand request, CancellationToken cancellationToken)
        {



            if (request.userId == Guid.Empty || request.amount <= 0)
            {
                throw new ArgumentException("not a valid input");
            }



            var user = await userRepository.GetUsersByIdAsync(request.userId);

            if (user == null)
                throw new ArgumentException("Invalid From User ID");

            return await transactionRepository.RemoveBalanceAsync(user, request.amount);


        }


    }
}
