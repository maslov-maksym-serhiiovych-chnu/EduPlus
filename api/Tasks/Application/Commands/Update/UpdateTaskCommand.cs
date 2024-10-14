using MediatR;

namespace Application.Commands.Update;

public record UpdateTaskCommand(int Id, string Name, string Description) : IRequest;