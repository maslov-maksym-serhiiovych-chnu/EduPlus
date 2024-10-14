using Domain.Models;
using Infrastructure.Persistence;
using MediatR;

namespace Application.Commands.Update;

public class UpdateTaskCommandHandler(TasksDbContext context) : IRequestHandler<UpdateTaskCommand>
{
    public async Task Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        TaskModel? task = await context.Tasks.FindAsync([request.Id], cancellationToken);
        if (task == null)
        {
            return;
        }

        task.Name = request.Task.Name;
        task.Description = request.Task.Description;
        await context.SaveChangesAsync(cancellationToken);
    }
}