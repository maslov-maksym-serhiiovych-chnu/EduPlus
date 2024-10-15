using Application.Commands.Create;
using Application.Commands.Delete;
using Application.Commands.Update;
using Application.Queries.Read;
using Application.Queries.ReadAll;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController(ISender mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<TaskModel>>> ReadAll()
    {
        var tasks = await mediator.Send(new ReadAllTasksQuery());
        return Ok(tasks);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TaskModel>> Read(int id)
    {
        TaskModel task = await mediator.Send(new ReadTaskQuery(id));
        return Ok(task);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<TaskModel>> Create([FromBody] TaskModel task)
    {
        int id = await mediator.Send(new CreateTaskCommand(task));
        return CreatedAtAction(nameof(Read), new { id }, task);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] TaskModel task)
    {
        await mediator.Send(new UpdateTaskCommand(id, task));

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        await mediator.Send(new DeleteTaskCommand(id));

        return NoContent();
    }
}