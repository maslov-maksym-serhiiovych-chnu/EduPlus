using Domain.Models;
using Infrastructure.Persistence;
using MediatR;

namespace Application.Commands.Delete;

public class DeleteTaskCommandHandler(TasksDbContext context) : IRequestHandler<DeleteTaskCommand>
{
    public async Task Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        TaskModel? task = await context.Tasks.FindAsync([request.Id], cancellationToken);
        if (task == null)
        {
            return;
        }

        context.Tasks.Remove(task);
        await context.SaveChangesAsync(cancellationToken);
    }
}