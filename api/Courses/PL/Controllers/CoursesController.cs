using BLL.Services;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoursesController(ICourseService service) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Course>>> ReadAll()
    {
        var recipes = await service.ReadAllAsync();
        return Ok(recipes);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Course>> Read(int id)
    {
        Course course = await service.ReadAsync(id);
        return Ok(course);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<Course>> Create(Course course)
    {
        int id = await service.CreateAsync(course);
        return CreatedAtAction(nameof(Read), new { id }, course);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, Course course)
    {
        await service.UpdateAsync(id, course);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        await service.DeleteAsync(id);

        return NoContent();
    }
}