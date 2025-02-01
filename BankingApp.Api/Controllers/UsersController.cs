using BankingApp.Application.Commands;
using BankingApp.Application.DTOs;
using BankingApp.Application.Queries;
using BankingApp.Core.DTOs;
using BankingApp.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace BankingApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(ISender sender) : ControllerBase
    {

        [Authorize]
        [HttpGet("profile")]
        public async Task<UserProfileDto> GetUserData()
        {
            Guid currentUserId = Guid.Parse(User.FindFirstValue("Id"));
            
            return await sender.Send (new GetUserByIdQuery(currentUserId));
             
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginDto loginDto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            LoginResponseDto response = await sender.Send(new LoginCommand(loginDto), cancellationToken);
            
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsersAsync([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var users = await sender.Send(new GetAllUserQuery(new PaginationDtos(page, pageSize)));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!users.Any())
                return NotFound("No users found.");

            return Ok(users);
        }


        // [HttpGet("{userId:guid}")]
        // public async Task<ActionResult<UserDto>> GetUserByIdAsync([FromRoute] Guid userId)
        // {
        //     var user = await sender.Send(new GetUserByIdQuery(userId));

        //     if (user == null)
        //         return NotFound($"User with ID {userId} not found.");

        //     return Ok(user);
        // }



        [HttpPost]
        public async Task<ActionResult<UserResponseDto>> RegisterUserAsync([FromBody] UserRegisterDto user)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            UserResponseDto createdUser = await sender.Send(new AddUserCommand(user));
            return Ok(createdUser);
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<UserResponseDto>> UpdateUserAsync( [FromBody] UserUpdateDto user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Guid currentUserId = Guid.Parse(User.FindFirstValue("Id"));

            UserResponseDto updatedUser = await sender.Send(new UpdateUserCommand(currentUserId, user));

            return Ok(updatedUser);
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteUserAsync()
        {
            Guid currentUserId = Guid.Parse(User.FindFirstValue("Id"));
            bool result = await sender.Send(new DeleteUserCommand(currentUserId));
            return NoContent(); 
        }
    }
}
