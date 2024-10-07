using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Blog.Data;
using Blog.Dtos;
using Blog.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Blog.Services
{
    public class UserService
    {
        private readonly ApplicationDbContext _context ;
        private readonly IPasswordHasher<UserModel> _passwordHasher;
        private readonly ITokenService _tokenService;

        public UserService(ApplicationDbContext context, IPasswordHasher<UserModel> passwordHasher,ITokenService tokenService)
        {
            _context= context ;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService ;
        }
        public async Task<UserDto> RegisterUserAsync(RegisterUserDto  registerUserDto)
        {
            if(await _context.Users.AnyAsync(u => u.Email == registerUserDto.Email))
            {
                throw new InvalidOperationException("Email is already in use.");
            }
            var user = new UserModel
            {
                UserName = registerUserDto.Username,
                Email = registerUserDto.Email,
                CreatedDate = DateTime.UtcNow
            };
            user.PasswordHash = _passwordHasher.HashPassword(user ,registerUserDto.Password);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return new UserDto
            {
                UserId = user.UserId ,
                Username = user.UserName,
                Email = user.Email,
                CreatedDate = user.CreatedDate
            };
        }
        public async Task<string>AuthenticationUserAsync(LoginUserDto loginUserDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginUserDto.Email);
            if(user == null)
            {
                throw new UnauthorizedAccessException("Invalid Login Attempt");

            }
            var passwordVerificationResult =  _passwordHasher.VerifyHashedPassword(user , user.PasswordHash ,loginUserDto.Password);
            if(passwordVerificationResult != PasswordVerificationResult.Success)
            {
                throw new UnauthorizedAccessException("Invalid Login Attempt");
            }
            var token = _tokenService.GenerateToken(user);
            return token;
        }
        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            var user =await _context.Users.FirstOrDefaultAsync(u =>u.UserId == id );
            if(user == null)
            {
                throw new InvalidOperationException("Users Not Found! ");

            }
            return new UserDto
            {
                UserId = user.UserId ,
                Username = user.UserName,
                Email = user.Email,
                CreatedDate= user.CreatedDate
            };
        }
        public async Task<bool> UpdateUserProfile(UserDto userDto)
        {
            var user =await _context.Users.FirstOrDefaultAsync(u => u.UserId == userDto.UserId);
            if(user == null)
            {
                return false;
            }
            user.UserName = userDto.Username;
            user.Email = userDto.Email;

            await _context.SaveChangesAsync();
            return true;
        }

        
    }
}