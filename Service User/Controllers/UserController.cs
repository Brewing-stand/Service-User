using Microsoft.AspNetCore.Mvc;
using Service_User.Models;
using Service_User.DTOs;
using Service_User.Repositories;
using AutoMapper;

namespace Service_User.Controllers
{
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

        // GET: api/<UserController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _userRepository.GetAllUsersAsync();
            if (result.IsSuccess)
            {
                // Map list of User models to list of UserResponseDto
                var userResponseDtos = _mapper.Map<IEnumerable<UserResponseDto>>(result.Value);
                return Ok(userResponseDtos);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _userRepository.GetUserByIdAsync(id);
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

        // POST api/<UserController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserRequestDto userRequestDto)
        {
            // Map UserRequestDto to User model
            var user = _mapper.Map<User>(userRequestDto);

            var result = await _userRepository.CreateUserAsync(user);
            if (result.IsSuccess)
            {
                // Map User model to UserResponseDto to return the response
                var userResponseDto = _mapper.Map<UserResponseDto>(result.Value);
                return CreatedAtAction(nameof(Get), new { id = userResponseDto.Id }, userResponseDto);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] UserRequestDto userRequestDto)
        {
            // Map UserRequestDto to User model
            var updatedUser = _mapper.Map<User>(userRequestDto);

            var result = await _userRepository.UpdateUserAsync(id, updatedUser);
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

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _userRepository.DeleteUserAsync(id);
            if (result.IsSuccess)
                return NoContent();  // 204 No Content
            else
                return NotFound(result.Errors);
        }
    }
}
