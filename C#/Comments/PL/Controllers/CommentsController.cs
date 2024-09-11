using DAL.Data;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class CommentsController(CommentRepository repository) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var comments = await repository.GetAllAsync();
        return Ok(comments);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Comment comment)
    {
        int commentId = await repository.CreateAsync(comment);
        return Ok(commentId);
    }
}