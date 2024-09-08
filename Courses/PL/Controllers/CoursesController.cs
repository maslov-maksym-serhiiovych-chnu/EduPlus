using DAL.Data.ADO.NET;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class CoursesController(CourseRepository courseRepository) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var courses = await courseRepository.GetAll();
        return Ok(courses);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        Course? course = await courseRepository.GetById(id);
        return course == null ? NotFound() : Ok(course);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Course course)
    {
        await courseRepository.Create(course);
        return CreatedAtAction(nameof(GetById), new { course.Id }, course);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateById(int id, [FromBody] Course course)
    {
        Course? existingCourse = await courseRepository.GetById(id);
        if (existingCourse == null)
        {
            return NotFound();
        }

        await courseRepository.UpdateById(id, course);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteById(int id)
    {
        Course? existingCourse = await courseRepository.GetById(id);
        if (existingCourse == null)
        {
            return NotFound();
        }

        await courseRepository.DeleteById(id);
        return NoContent();
    }
}