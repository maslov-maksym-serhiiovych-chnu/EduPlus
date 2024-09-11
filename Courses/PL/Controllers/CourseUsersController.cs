using DAL.Data;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class CourseUsersController(DapperCourseUserRepository repository) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await repository.GetAllAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        CourseUser? courseUser = await repository.GetAsync(id);
        return courseUser == null ? NotFound() : Ok(courseUser);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CourseUser courseUser)
    {
        courseUser.Id = await repository.CreateAsync(courseUser);
        return CreatedAtAction(nameof(GetById), new { courseUser.Id }, courseUser);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateById(int id, [FromBody] CourseUser courseUser)
    {
        if (await repository.GetAsync(id) == null)
        {
            return NotFound();
        }

        await repository.UpdateAsync(id, courseUser);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteById(int id)
    {
        if (await repository.GetAsync(id) == null)
        {
            return NotFound();
        }

        await repository.DeleteAsync(id);
        return NoContent();
    }
}