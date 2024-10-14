using MediatR;

namespace Application.Commands.Create;

public record CreateTaskCommand(int Id, string Name, string Description) : IRequest<int>;