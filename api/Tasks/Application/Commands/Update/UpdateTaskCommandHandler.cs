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

        task.Name = request.Name;
        task.Description = request.Description;
        await context.SaveChangesAsync(cancellationToken);
    }
}