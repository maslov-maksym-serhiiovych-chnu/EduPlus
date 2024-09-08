using DAL.Data;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class DapperCourseUsersController(DapperCourseUserRepository dapperCourseUserRepository) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var courseUsers = await dapperCourseUserRepository.GetAll();
        return Ok(courseUsers);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        CourseUser? courseUser = await dapperCourseUserRepository.GetById(id);
        return courseUser == null ? NotFound() : Ok(courseUser);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CourseUser courseUser)
    {
        await dapperCourseUserRepository.Create(courseUser);
        return CreatedAtAction(nameof(GetById), new { courseUser.Id }, courseUser);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateById(int id, [FromBody] CourseUser courseUser)
    {
        CourseUser? existingCourseUser = await dapperCourseUserRepository.GetById(id);
        if (existingCourseUser == null)
        {
            return NotFound();
        }

        await dapperCourseUserRepository.UpdateById(id, courseUser);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteById(int id)
    {
        CourseUser? existingCourseUser = await dapperCourseUserRepository.GetById(id);
        if (existingCourseUser == null)
        {
            return NotFound();
        }

        await dapperCourseUserRepository.DeleteById(id);
        return NoContent();
    }
}