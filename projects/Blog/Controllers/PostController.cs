using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Dtos;
using Blog.Services;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController:ControllerBase
    {
        private readonly IPostService _postService;
        public PostController(IPostService postService)
        {
            _postService = postService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostDto>>>GetPosts()
        {
            var posts = await _postService.GetAllPostsAsync();
            return Ok(posts);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<PostDto>> GetPostById(int id)
        {
            var post =await _postService.GetPostByIdAsync(id);
            if(post == null)
            {
                return NotFound();
            }
            return Ok(post);
        }
        [HttpPost]
        public async Task<ActionResult<PostDto>>CreatePost([FromBody] PostDto postDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createPost =await _postService.CreatePostAsync(postDto);
            return CreatedAtAction(nameof(GetPostById) , new{id = createPost.PostId} , createPost);

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost(int id , [FromBody]PostDto postDto)
        {
            if(id != postDto.PostId)
            {
                return BadRequest("Post Id Mismatch");
            }
            var result = await _postService.UpdatePostAsync(postDto);
            if(!result)
            {
                return NotFound();
            }
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var result = await _postService.DeletePostAsync(id);
            if(!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}