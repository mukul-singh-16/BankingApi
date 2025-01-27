using Azure.Core;
using BankingApp.Application.Commands;
using BankingApp.Application.DTOs;
using BankingApp.Application.Queries;
using BankingApp.Core.DTOs;
using BankingApp.Core.Entities;
using BankingApp.Core.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BankingApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(ISender sender) : ControllerBase
    {
        //private readonly IUserRepository _userRepository;

        //public UsersController(IUserRepository userRepository)
        //{
        //    Console.WriteLine("users");
        //    _userRepository = userRepository;
        //}




        [HttpGet]
        public async Task<IActionResult> GetAllUsersAsync([FromQuery] int page =1  , [FromQuery] int pageSize = 10)
        {

            if (page <= 0 || pageSize <= 0)
                throw new Exception(message: "pagination data is incorrect check it again");

            var users = await sender.Send(new GetAllUserQuery(new PaginationDtos((int)page, (int)pageSize)));

            
            return Ok(users);
        }



        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserByIdAsync([FromRoute] Guid userId)
        {
            var user = await sender.Send(new GetUserByIdQuery(userId));

            if (user == null)
            {
                return NotFound($"User with ID {userId} not found.");
            }
            return Ok(user);
        }





        [HttpPost]
        public async Task<IActionResult> AddUserAsync([FromBody] UserRequestDtos user)
        {

            if (user == null)
            {
                return BadRequest("User data cannot be null.");
            }

            try
            {
                var result = await sender.Send(new AddUserCommand(user));

                return Ok(result);


            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error adding user: {ex.Message}");
            }
        }

        

        [HttpPut("{userId:guid}")]
        public async Task<IActionResult> UpdateUserAsync([FromRoute] Guid userId, [FromBody] UserRequestDtos user)
        {
            if (user == null)
            {
                return BadRequest("User data cannot be null.");
            }

            try
            {
                var updatedUser = await sender.Send(new UpdateUserCommand(userId, user));

                if (updatedUser == null)
                {
                    return NotFound($"User with ID {userId} not found.");
                }

                return Ok(updatedUser);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating user: {ex.Message}");
            }
        }


        [HttpDelete("{userId:guid}")]
        public async Task<IActionResult> DeleteUserAsync([FromRoute] Guid userId)
        {
            bool result = await sender.Send(new DeleteUserCommand(userId));

            if (result)
            {
                return NoContent();
            }

            return NotFound($"User with ID {userId} not found.");
        }
    }
}
