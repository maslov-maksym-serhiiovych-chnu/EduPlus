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

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        Comment? comment = await repository.GetAsync(id);
        if (comment == null)
        {
            return NotFound();
        }

        return Ok(comment);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Comment comment)
    {
        comment.Id = await repository.CreateAsync(comment);

        object routeValue = new { id = comment.Id };
        return CreatedAtAction(nameof(Create), routeValue, comment);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] Comment comment)
    {
        Comment? existing = await repository.GetAsync(id);
        if (existing == null)
        {
            return NotFound();
        }

        await repository.UpdateAsync(id, comment);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        Comment? comment = await repository.GetAsync(id);

        if (comment == null)
        {
            return NotFound();
        }

        await repository.DeleteAsync(id);
        return NoContent();
    }
}