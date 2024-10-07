using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Dtos;
using Blog.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    [Microsoft.AspNetCore.Components.Route("api/[controller]")]
    [ApiController]
    public class UserController:ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto registerUserDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(); 
            }
            try{
                var userDto = await _userService.RegisterUserAsync(registerUserDto);
                return Ok(userDto);
            }
            catch (System.InvalidCastException ex)
            {
                return BadRequest(new {message = ex.Message});

            }

        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto loginUserDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            try 
            {
                var token = await _userService.AuthenticateUserAsync(loginUserDto);
                return Ok(new {token});
            }
            catch(System.UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message});
            }
        }
        [Authorize]
        [HttpGet("profile")]
        public async Task<IActionResult> GetUserProfile()
        {
            var userId = int.Parse(User.FindFirst("sub").Value);
            var userDto = await _userService.GetUserByIdAsync(userId);

            if(userDto == null)
            {
                return NotFound();
            }
            return Ok(userDto);
        }
        [Authorize]
        [HttpPost("profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UserDto userDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userId = int.Parse(User.FindFirst("sub").Value);
            userDto.UserId = userId;

            var result = await _userService.UpdateUserProfileAsync(userDto);
            if(!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}