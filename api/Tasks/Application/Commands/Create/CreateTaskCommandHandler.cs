using Domain.Models;
using Infrastructure.Persistence;
using MediatR;

namespace Application.Commands.Create;

public class CreateTaskCommandHandler(TasksDbContext context) : IRequestHandler<CreateTaskCommand, int>
{
    public async Task<int> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        TaskModel task = new()
        {
            Name = request.Name,
            Description = request.Description
        };
        await context.Tasks.AddRangeAsync(task);
        int id = await context.SaveChangesAsync(cancellationToken);
        return id;
    }
}