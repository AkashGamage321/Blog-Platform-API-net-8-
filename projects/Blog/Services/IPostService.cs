using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Dtos;

namespace Blog.Services
{
    public interface IPostService
    {
        Task<IEnumerable<PostDto>> GetAllPostsAsync();
        Task<PostDto> GetPostByIdAsync(int id);
        Task<PostDto> CreatePostAsync(PostDto postDto);
        Task<bool> UpdatePostAsync(PostDto postDto);
        Task<bool> DeletePostAsync(int id);
    }
}