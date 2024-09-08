using DAL.Data;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class AdoDotNetCourseUsersController(AdoDotNetCourseUserRepository adoDotNetCourseUserRepository)
    : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var courseUsers = await adoDotNetCourseUserRepository.GetAll();
        return Ok(courseUsers);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        CourseUser? courseUser = await adoDotNetCourseUserRepository.GetById(id);
        return courseUser == null ? NotFound() : Ok(courseUser);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CourseUser courseUser)
    {
        await adoDotNetCourseUserRepository.Create(courseUser);
        return CreatedAtAction(nameof(GetById), new { courseUser.Id }, courseUser);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateById(int id, [FromBody] CourseUser courseUser)
    {
        CourseUser? existingCourseUser = await adoDotNetCourseUserRepository.GetById(id);
        if (existingCourseUser == null)
        {
            return NotFound();
        }

        await adoDotNetCourseUserRepository.UpdateById(id, courseUser);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteById(int id)
    {
        CourseUser? existingCourseUser = await adoDotNetCourseUserRepository.GetById(id);
        if (existingCourseUser == null)
        {
            return NotFound();
        }

        await adoDotNetCourseUserRepository.DeleteById(id);
        return NoContent();
    }
}