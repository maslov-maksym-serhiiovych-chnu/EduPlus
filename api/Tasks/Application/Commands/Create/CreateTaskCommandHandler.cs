using Infrastructure.Persistence;
using MediatR;

namespace Application.Commands.Create;

public class CreateTaskCommandHandler(TasksDbContext context) : IRequestHandler<CreateTaskCommand, int>
{
    public async Task<int> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        await context.Tasks.AddRangeAsync(request.Task);
        int id = await context.SaveChangesAsync(cancellationToken);
        return id;
    }
}