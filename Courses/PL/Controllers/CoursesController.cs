using DAL.Data;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class CoursesController(DapperCourseRepository repository) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await repository.GetAllAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        Course? course = await repository.GetAsync(id);
        return course == null ? NotFound() : Ok(course);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Course course)
    {
        course.Id = await repository.CreateAsync(course);
        return CreatedAtAction(nameof(GetById), new { course.Id }, course);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] Course course)
    {
        if (await repository.GetAsync(id) == null)
        {
            return NotFound();
        }

        await repository.UpdateAsync(id, course);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (await repository.GetAsync(id) == null)
        {
            return NotFound();
        }

        await repository.DeleteAsync(id);
        return NoContent();
    }
}