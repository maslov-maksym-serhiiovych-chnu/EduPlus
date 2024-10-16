﻿using Domain.Exceptions;
using Domain.Models;
using Infrastructure.Data;
using MediatR;

namespace Application.Commands.Delete;

public class DeleteTaskCommandHandler(TasksDbContext context) : IRequestHandler<DeleteTaskCommand>
{
    public async Task Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        TaskModel? task = await context.Tasks.FindAsync([request.Id], cancellationToken);
        if (task == null)
        {
            throw new TaskNotFoundException("task not found by id: " + request.Id);
        }

        context.Tasks.Remove(task);
        await context.SaveChangesAsync(cancellationToken);
    }
}