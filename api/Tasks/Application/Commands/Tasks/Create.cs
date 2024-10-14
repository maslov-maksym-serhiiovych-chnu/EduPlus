using MediatR;

namespace Application.Commands.Tasks;

public record Create(int Id, string Name, string Description) : IRequest<int>;