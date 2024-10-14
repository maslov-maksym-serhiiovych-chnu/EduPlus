using MediatR;

namespace Application.Commands.Tasks;

public record UpdateTask(int Id, string Name, string Description) : IRequest;