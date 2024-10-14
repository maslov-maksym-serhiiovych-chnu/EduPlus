using Domain.Models;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.ReadAll;

public class ReadAllTasksQueryHandler(TasksDbContext context)
    : IRequestHandler<ReadAllTasksQuery, IEnumerable<TaskModel>>
{
    public async Task<IEnumerable<TaskModel>> Handle(ReadAllTasksQuery request, CancellationToken cancellationToken)
    {
        var tasks = await context.Tasks.ToArrayAsync(cancellationToken);
        return tasks;
    }
}