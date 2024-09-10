using MediatR;
using ReflectionExample.Api.Commands;

namespace ReflectionExample.Handlers.Events
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TCommand"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="Comanda"></param>
    /// <param name="Result"></param>
    public record BaseEvent<TCommand, TResult>(TCommand Comanda, TResult Result) : INotification where TCommand : IRequest<TResult>;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="command"></param>
    public record CommandExample1UnitEvent(CommandExample1Unit command) : BaseEvent<CommandExample1Unit, Unit>(command, Unit.Value);
}
