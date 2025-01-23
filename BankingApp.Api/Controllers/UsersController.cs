using BankingApp.Core.DTOs;
using BankingApp.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BankingApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            Console.WriteLine("users");
            _userRepository = userRepository;
        }




        [HttpGet]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var users = await _userRepository.GetUsers();
            return Ok(users);

        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetUserByIdAsync(Guid id)
        {
            var user = await _userRepository.GetUsersByIdAsync(id);
            if (user == null)
            {
                return NotFound($"User with ID {id} not found.");
            }
            return Ok(user);
        }

        
        


        [HttpPost]
        public async Task<IActionResult> AddUserAsync([FromBody] UserRequest user)
        {
            if (user == null)
            {
                return BadRequest("User data cannot be null.");
            }

            try
            {
                var newUser = await _userRepository.AddUserAsync(user);

                return Ok(newUser);

                
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error adding user: {ex.Message}");
            }
        }

        

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateUserAsync(Guid id, [FromBody] UserRequest user)
        {
            if (user == null)
            {
                return BadRequest("User data cannot be null.");
            }

            try
            {
                var updatedUser = await _userRepository.UpdateUserAsync(id, user);

                if (updatedUser == null)
                {
                    return NotFound($"User with ID {id} not found.");
                }

                return Ok(updatedUser);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating user: {ex.Message}");
            }
        }


        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteUserAsync(Guid id)
        {
            bool result = await _userRepository.DeleteUserAsync(id);

            if(result)
            {
                return NoContent();
            }
            return NotFound($"User with ID {id} not found.");
        }
    }
}
