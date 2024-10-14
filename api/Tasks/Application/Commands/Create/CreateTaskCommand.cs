using MediatR;

namespace Application.Commands.Create;

public record CreateTaskCommand(string Name, string Description) : IRequest<int>;