using Microsoft.AspNetCore.Mvc;

namespace apib.Controllers;
using apib.Models;

using apib.Data;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("[controller]")]
public class PostController : ControllerBase
{

     private static ApiDbContext _context;

    public PostController(ApiDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var posts = await _context.Posts.ToListAsync();
        return Ok(posts);
    }

     [HttpGet("{postId:int}")]
    public async Task<IActionResult> Get(int postId)
    {
        var post = await _context.Posts.FirstOrDefaultAsync(p => p.PostId == postId);

        if (post == null)
            return BadRequest("Invalid Id");

        return Ok(post);
    }

    [HttpPost]
    public async Task<IActionResult> Post(Post post)
    {
        await _context.Posts.AddAsync(post);
        await _context.SaveChangesAsync();

        return CreatedAtAction("Post", post.PostId, post);
    }

    [HttpPatch]
    public async Task<IActionResult> Patch(int postId, string title)
    {
        var post = await _context.Posts.FirstOrDefaultAsync(p => p.PostId == postId);

        if (post == null)
            return BadRequest("Invalid id");

        post.Title = title;
        await _context.SaveChangesAsync();

        return NoContent();
    }
    
    [HttpPut]
    public async Task<IActionResult> Put(int postId, Post updatedPost)
    {
        var post = await _context.Posts.FirstOrDefaultAsync(p => p.PostId == postId);

        if (post == null)
            return BadRequest("Invalid id");

        post.Title = updatedPost.Title;
        post.Content = updatedPost.Content;
        
        await _context.SaveChangesAsync();

        return NoContent();
    }
    
    [HttpDelete]
    public async Task<IActionResult> Delete(int postId)
    {
        var post = await _context.Posts.FirstOrDefaultAsync(p => p.PostId == postId);

        if (post == null)
            return BadRequest("Invalid id");

        _context.Posts.Remove(post);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    

   
}