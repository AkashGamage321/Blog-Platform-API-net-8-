using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Dtos
{
    public class PostDto
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string UserName { get;set; }
        public DateTime CreatedDate { get;set ;}
    }
}