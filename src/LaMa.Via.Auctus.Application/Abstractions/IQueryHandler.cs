using ErrorOr;
using MediatR;

namespace LaMa.Via.Auctus.Application.Abstractions;

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, ErrorOr<TResponse>>
    where TQuery : IQuery<TResponse>
{
}