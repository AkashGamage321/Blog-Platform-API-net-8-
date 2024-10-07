using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Dtos;

namespace Blog.Services
{
    public interface IUserService
    {
        Task<UserDto>RegisterUserAsync(RegisterUserDto registerUserDto);
        Task<string> AuthenticateUserAsync(LoginUserDto loginUserDto);

        // Method to get a user by their ID
        Task<UserDto> GetUserByIdAsync(int id);

        // Method to update user profile information
        Task<bool> UpdateUserProfileAsync(UserDto userDto);

    }
}