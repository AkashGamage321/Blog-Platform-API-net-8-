using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Models
{
    public class CommentModel
    {
        public int CommentId { get;set; }
        public int PostId { get;set; }
        public PostModel Post { get;set; }
        public int UserId { get;set; }
        public UserModel User { get;set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get;set; } 
    }
}
