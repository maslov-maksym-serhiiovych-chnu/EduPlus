using MediatR;

namespace Application.Commands.Tasks;

public record Update(int Id, string Name, string Description) : IRequest;