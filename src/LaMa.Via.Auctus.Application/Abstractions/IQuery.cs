using ErrorOr;
using MediatR;

namespace LaMa.Via.Auctus.Application.Abstractions;

public interface IQuery<TResponse> : IRequest<ErrorOr<TResponse>>
{
}