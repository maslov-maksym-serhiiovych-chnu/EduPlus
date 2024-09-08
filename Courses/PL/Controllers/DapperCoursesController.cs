using DAL.Data;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class DapperCoursesController(DapperCourseRepository dapperCourseRepository) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var courses = await dapperCourseRepository.GetAll();
        return Ok(courses);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        Course? course = await dapperCourseRepository.GetById(id);
        return course == null ? NotFound() : Ok(course);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Course course)
    {
        await dapperCourseRepository.Create(course);
        return CreatedAtAction(nameof(GetById), new { course.Id }, course);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateById(int id, [FromBody] Course course)
    {
        Course? existingCourse = await dapperCourseRepository.GetById(id);
        if (existingCourse == null)
        {
            return NotFound();
        }

        await dapperCourseRepository.UpdateById(id, course);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteById(int id)
    {
        Course? existingCourse = await dapperCourseRepository.GetById(id);
        if (existingCourse == null)
        {
            return NotFound();
        }

        await dapperCourseRepository.DeleteById(id);
        return NoContent();
    }
}