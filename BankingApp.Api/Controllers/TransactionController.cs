using BankingApp.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BankingApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionController(ITransactionRepository transactionRepository)
        {
            //Console.WriteLine("transactionrepo");
            _transactionRepository = transactionRepository;
        }


        //[HttpGet]
        // public async Task<IActionResult> Hello()
        // {
            
        //    return Ok("hello");
        // }

        [HttpPost("transfer")]
        public async Task<IActionResult> TransferBalance([FromQuery] Guid fromUserId, [FromQuery] Guid toUserId, [FromQuery] decimal amount)
        {
            if (amount <= 0)
                return BadRequest("Amount must be greater than zero.");

            var success = await _transactionRepository.TransferBalanceAsync(fromUserId, toUserId, amount);
            if (success)
                return Ok("Transfer successful.");
            return BadRequest("Transfer failed.");
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddBalance([FromQuery] Guid userId, [FromQuery] decimal amount)
        {
            if (amount <= 0)
                return BadRequest("Amount must be greater than zero.");

            bool success = await _transactionRepository.AddBalanceAsync(userId, amount);
            if (success)
                return Ok("Balance added successfully.");

            return BadRequest("Failed to add balance.");
        }

        [HttpPost("remove")]
        public async Task<IActionResult> RemoveBalance([FromQuery] Guid userId, [FromQuery] decimal amount)
        {

            if (amount <= 0)
                return BadRequest("Amount must be greater than zero.");

            var success = await _transactionRepository.RemoveBalanceAsync(userId, amount);
            if (success)
                return Ok("Balance removed successfully.");
            return BadRequest("Failed to remove balance.");
        }


        

        [HttpGet("{userId}/history")]
        public async Task<IActionResult> GetTransactionHistory(Guid userId, [FromQuery] int page = 1, [FromQuery] int pageSize = 3)
        {
            if (page <= 0 || pageSize <= 0)
                return BadRequest("Page and PageSize must be greater than zero.");

            var transactions = await _transactionRepository.GetTransactionHistoryAsync(userId, page, pageSize);
            
            if (!transactions.Any())
                return NotFound("No transactions found for the user.");

            return Ok(transactions);
        }

            }
        }
