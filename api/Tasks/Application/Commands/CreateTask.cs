using MediatR;

namespace Application.Commands.Tasks;

public record CreateTask(int Id, string Name, string Description) : IRequest<int>;