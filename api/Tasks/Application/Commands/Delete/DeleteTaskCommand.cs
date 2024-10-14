using MediatR;

namespace Application.Commands.Delete;

public record DeleteTaskCommand(int Id) : IRequest;