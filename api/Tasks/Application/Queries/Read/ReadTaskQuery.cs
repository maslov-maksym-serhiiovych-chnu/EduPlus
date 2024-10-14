using Domain.Models;
using MediatR;

namespace Application.Queries.Read;

public record ReadTaskQuery(int Id) : IRequest<TaskModel>;