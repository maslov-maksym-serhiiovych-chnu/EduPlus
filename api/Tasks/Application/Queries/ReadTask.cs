using Domain.Models;
using MediatR;

namespace Application.Queries;

public record ReadTask(int Id) : IRequest<TaskModel>;