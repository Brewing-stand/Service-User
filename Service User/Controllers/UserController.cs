using Microsoft.AspNetCore.Mvc;
using Service_User.Models;
using Service_User.DTOs;
using Service_User.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using FluentResults;

namespace Service_User.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets the authenticated user's ID from the JWT.
        /// </summary>
        private Result<Guid> GetUserId()
        {
            var idClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Guid.TryParse(idClaim, out var userId) ? Result.Ok(userId) : Result.Fail<Guid>("Invalid user ID.");
        }

        // GET api/<UserController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userIdResult = GetUserId();
            if (userIdResult.IsFailed)
                return Unauthorized(userIdResult.Errors);

            var result = await _userRepository.GetUserByIdAsync(userIdResult.Value, userIdResult.Value);
            if (result.IsSuccess)
            {
                // Map User model to UserResponseDto
                var userResponseDto = _mapper.Map<UserResponseDto>(result.Value);
                return Ok(userResponseDto);
            }
            else
            {
                return NotFound(result.Errors);
            }
        }

        // PUT api/<UserController>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UserRequestDto userRequestDto)
        {
            var userIdResult = GetUserId();
            if (userIdResult.IsFailed)
                return Unauthorized(userIdResult.Errors);

            // Map UserRequestDto to User model
            var updatedUser = _mapper.Map<User>(userRequestDto);

            var result = await _userRepository.UpdateUserAsync(userIdResult.Value, userIdResult.Value, updatedUser);
            if (result.IsSuccess)
            {
                // Map updated User model to UserResponseDto
                var userResponseDto = _mapper.Map<UserResponseDto>(result.Value);
                return Ok(userResponseDto);
            }
            else
            {
                return NotFound(result.Errors);
            }
        }

        // DELETE api/<UserController>
        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            var userIdResult = GetUserId();
            if (userIdResult.IsFailed)
                return Unauthorized(userIdResult.Errors);

            var result = await _userRepository.DeleteUserAsync(userIdResult.Value, userIdResult.Value);
            if (result.IsSuccess)
                return NoContent(); // 204 No Content
            else
                return NotFound(result.Errors);
        }
    }
}
