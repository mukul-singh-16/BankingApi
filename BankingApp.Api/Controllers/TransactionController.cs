using BankingApp.Application.Commands;
using BankingApp.Application.DTOs;
using BankingApp.Application.Queries;
using BankingApp.
    Core.DTOs;
using BankingApp.Core.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BankingApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController(ISender sender) : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> Hello()
        {
            return Ok("hello");
        }



        [HttpPost("transfer")]
        public async Task<IActionResult> TransferBalance([FromBody] TransferBalanceDtos body )
        {
            
            var success = await sender.Send( new TransferBalanceCommand(body));

            if (success)
                return Ok("Transfer successful.");

            return BadRequest("Transfer failed.");
        }




       
        [HttpPost("add")]
        public async Task<IActionResult> AddBalance([FromBody] BalanceRequestDtos body)
        {
            if (body.Amount <= 0)
                return BadRequest("Amount must be greater than zero.");

            bool success = await sender.Send(new RemoveBalanceCommand(body.UserId,body.Amount));
            if (success)
                return Ok("Balance added successfully.");

            return BadRequest("Failed to add balance.");
        }

        [HttpPost("remove")]
        public async Task<IActionResult> RemoveBalance([FromBody] BalanceRequestDtos body)
        {
            if (body.Amount <= 0)
                return BadRequest("Amount must be greater than zero.");

            var success = await sender.Send(new RemoveBalanceCommand(body.UserId, body.Amount));
            if (success)
                return Ok("Balance removed successfully.");

            return BadRequest("Failed to remove balance.");
        }




        [HttpGet("{userId}/history")]
        public async Task<IActionResult> GetTransactionHistory(Guid userId, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (page <= 0 || pageSize <= 0)
                return BadRequest("Page and PageSize must be greater than zero.");

            var transactions = await  sender.Send(new GetTransactionHistoryQuery(userId, new PaginationDtos(page, pageSize)));
            
            if (!transactions.Any())
                return NotFound("No transactions found for the user.");

            return Ok(transactions);
        }

            }
        }
