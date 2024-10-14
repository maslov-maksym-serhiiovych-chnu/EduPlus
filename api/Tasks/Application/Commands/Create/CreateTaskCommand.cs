using Domain.Models;
using MediatR;

namespace Application.Commands.Create;

public record CreateTaskCommand(TaskModel Task) : IRequest<int>;