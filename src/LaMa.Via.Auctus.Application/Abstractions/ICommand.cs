using ErrorOr;
using MediatR;

namespace LaMa.Via.Auctus.Application.Abstractions;

public interface IBaseCommand
{
}

public interface ICommand : IRequest<IErrorOr>, IBaseCommand
{
}

public interface ICommand<T> : IRequest<ErrorOr<T>>, IBaseCommand
{
}