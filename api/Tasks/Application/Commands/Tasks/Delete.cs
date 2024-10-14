using MediatR;

namespace Application.Commands.Tasks;

public record Delete(int Id) : IRequest;