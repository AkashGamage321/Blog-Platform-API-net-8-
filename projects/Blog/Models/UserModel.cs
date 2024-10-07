using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
// using 

namespace Blog.Models
{
    public class UserModel:IdentityUser
    {
        // [Key]
        public int UserId { get;set; }
        public string UserName { get; set; }
        public string Email { get;set; }
        public string PasswordHash { get;set; }
        public DateTime CreatedDate { get;set; }
    }
}