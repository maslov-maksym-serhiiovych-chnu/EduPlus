using DAL.Data;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class CoursesController(CourseRepositoryTemp courseRepositoryTemp) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var courses = await courseRepositoryTemp.GetAll();
        return Ok(courses);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        Course? course = await courseRepositoryTemp.GetById(id);
        return course == null ? NotFound() : Ok(course);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Course course)
    {
        await courseRepositoryTemp.Create(course);
        return CreatedAtAction(nameof(GetById), new { course.Id }, course);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateById(int id, [FromBody] Course course)
    {
        Course? existingCourse = await courseRepositoryTemp.GetById(id);
        if (existingCourse == null)
        {
            return NotFound();
        }

        await courseRepositoryTemp.UpdateById(id, course);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteById(int id)
    {
        Course? existingCourse = await courseRepositoryTemp.GetById(id);
        if (existingCourse == null)
        {
            return NotFound();
        }

        await courseRepositoryTemp.DeleteById(id);
        return NoContent();
    }
}