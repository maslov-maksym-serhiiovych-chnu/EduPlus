﻿using Domain.Exceptions;
using Domain.Models;
using Infrastructure.Data;
using MediatR;

namespace Application.Queries.Read;

public class ReadTaskQueryHandler(TasksDbContext context) : IRequestHandler<ReadTaskQuery, TaskModel>
{
    public async Task<TaskModel> Handle(ReadTaskQuery request, CancellationToken cancellationToken)
    {
        TaskModel? task = await context.Tasks.FindAsync([request.Id], cancellationToken);
        return task ?? throw new TaskNotFoundException("task not found by id: " + request.Id);
    }
}