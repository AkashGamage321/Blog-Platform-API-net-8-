using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Models;
namespace Blog.Services
{
    public interface ITokenService
    {
        string GenerateToken (UserModel user );

    }
}