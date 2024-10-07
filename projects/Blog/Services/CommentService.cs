using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Data;
using Blog.Dtos;
using Blog.Models;

namespace Blog.Services
{
    public class CommentService
    {
        private readonly ApplicationDbContext _context;
        public CommentService(ApplicationDbContext context)
        {
            _context =context;
        }
        public async Task<CommentDto>AddCommentAsync(AddCommentDto addCommentDto)
        {
            var comment = new CommentModel
            {
                PostId = addCommentDto.PostId , 
                UserId = a
            }
        }
    }
}