using Domain.Models;
using MediatR;

namespace Application.Queries;

public record ReadAllTasks : IRequest<IEnumerable<TaskModel>>;