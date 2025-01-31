using System.Security.Claims;
using BankingApp.Application.Commands;
using BankingApp.Application.DTOs;
using BankingApp.Application.Queries;
using BankingApp.
    Core.DTOs;
using BankingApp.Core.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankingApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController(ISender sender) : ControllerBase
    {
        [Authorize]
        [HttpPost("transfer")]
        public async Task<IActionResult> TransferBalance([FromBody] TransferBalanceDtos transferBalanceDtos)
        {
            Guid currentUserId = Guid.Parse(User.FindFirstValue("Id"));

            transferBalanceDtos.FromUserId = currentUserId;

            var updatedBalance = await sender.Send(new TransferBalanceCommand(transferBalanceDtos));

            
                return Ok(new { Message = "Transfer successful.", Balance = updatedBalance });

            
        }





        [Authorize]
        [HttpPost("add")]
        public async Task<IActionResult> AddBalance([FromBody] BalanceRequestDtos body)
        {
            var updatedBalance = await sender.Send(new AddBalanceCommand(body.UserId, body.Amount));

            return Ok(new { Message = "Balance added successfully.", Balance = updatedBalance });
        }





        [Authorize]
        [HttpPost("remove")]
        public async Task<IActionResult> RemoveBalance([FromBody] BalanceRequestDtos body)
        {
            var updatedBalance = await sender.Send(new RemoveBalanceCommand(body.UserId, body.Amount));

            
                return Ok(new { Message = "Balance removed successfully.", Balance = updatedBalance });

            
        }




        [Authorize]
        [HttpGet("history")]
        public async Task<IActionResult> GetTransactionHistory( [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {

            Guid currentUserId = Guid.Parse(User.FindFirstValue("Id"));

            

            var transactions = await sender.Send(new GetTransactionHistoryQuery(currentUserId, new PaginationDtos(page, pageSize)));

            if (!transactions.Any())
                return NotFound("No transactions found for the user.");

            return Ok(transactions);
        }


    }
}

