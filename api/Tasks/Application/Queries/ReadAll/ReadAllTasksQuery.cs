using Domain.Models;
using MediatR;

namespace Application.Queries.ReadAll;

public record ReadAllTasksQuery : IRequest<IEnumerable<TaskModel>>;