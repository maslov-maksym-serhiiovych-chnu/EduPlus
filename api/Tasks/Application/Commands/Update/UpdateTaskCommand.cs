using Domain.Models;
using MediatR;

namespace Application.Commands.Update;

public record UpdateTaskCommand(int Id, TaskModel Task) : IRequest;