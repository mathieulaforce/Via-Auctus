using ErrorOr;
using MediatR;

namespace LaMa.Via.Auctus.Application.Abstractions;

public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, IErrorOr>
    where TCommand : ICommand
{
}

public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, ErrorOr<TResponse>>
    where TCommand : ICommand<TResponse>
{
}