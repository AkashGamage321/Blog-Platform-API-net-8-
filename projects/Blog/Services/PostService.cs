using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Data;
using Blog.Dtos;
using Blog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Blog.Services
{
    public class PostService:IPostService
    {
        private readonly ApplicationDbContext _context ;
        public PostService(ApplicationDbContext context)
        {
            _context =context;
        }

        public async Task<IEnumerable<PostDto>> GetAllPostsAsync()
        {
            var posts = await _context.Posts.Include(p=>p.User).ToListAsync();
            var postDtos = posts.Select(post => new PostDto
            {
                PostId = post.PostId,
                UserId = post.UserId,
                Title = post.Title,
                Content = post.Content,
                CreatedDate = post.CreatedDate
            });
            return postDtos;
        }

        public async Task<PostDto> GetPostByIdAsync(int id)
        {
            var post = await _context.Posts
            .Include(p =>p.User)
            .FirstOrDefaultAsync(p =>p.PostId ==id);

            if(post == null)
            {
                return null;
            }
            var postDto = new PostDto
            {
                PostId = post.PostId,
                UserId = post.UserId,
                Title = post.Title,
                Content = post.Content,
                CreatedDate = post.CreatedDate
            };
            return postDto;
        }
        
        public async Task<PostDto>CreatePostAsync(PostDto postDto)
        {
            var post = new PostModel
            {
                UserId = postDto.UserId ,
                Title = postDto.Title , 
                Content = postDto.Content ,
                CreatedDate = DateTime.UtcNow
            };
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            postDto.PostId = post.PostId ;
            postDto.CreatedDate = post.CreatedDate;
            return postDto;
        }
        public async Task<bool> UpdatePostAsync(PostDto postDto)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(p =>p.PostId == postDto.PostId);
            if(post == null)
            {
                return false ;
            }
            post.Title = postDto.Title;
            post.Content = postDto.Content ;
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeletePostAsync(int id )
        {
            var post = await _context.Posts.FirstOrDefaultAsync(p=>p.PostId == id);
            if(post == null)
            {
                return false;
            }
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return true;
        }


    }
}