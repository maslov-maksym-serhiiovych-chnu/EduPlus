using MediatR;

namespace Application.Commands.Tasks;

public record DeleteTask(int Id) : IRequest;