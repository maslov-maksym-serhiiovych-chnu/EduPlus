using DAL.Data;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PL.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class CoursesController(CoursesDbContext context) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<Course>> GetAll()
    {
        var courses = await context.Courses.ToListAsync();
        return Ok(courses);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Course>> Get(int id)
    {
        Course? course = await context.Courses.FindAsync(id);
        return course == null ? NotFound() : Ok(course);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<Course>> Create([FromBody] Course course)
    {
        await context.Courses.AddAsync(course);
        await context.SaveChangesAsync();

        return CreatedAtAction(nameof(Get), new { id = course.Id }, course);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] Course course)
    {
        Course? updated = await context.Courses.FindAsync(id);
        if (updated == null)
        {
            return NotFound();
        }

        updated.Name = course.Name;
        updated.Description = course.Description;

        await context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        Course? course = await context.Courses.FindAsync(id);
        if (course == null)
        {
            return NotFound();
        }

        context.Courses.Remove(course);
        await context.SaveChangesAsync();
        return NoContent();
    }
}