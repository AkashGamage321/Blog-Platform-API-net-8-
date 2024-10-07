using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Dtos
{
    public class AddCommentDto
    {
        public int PostId { get;set ; }
        public int UserId { get;set; }
        public string Content { get;set; }
    }
}