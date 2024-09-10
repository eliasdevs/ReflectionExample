using MediatR;
using ReflectionExample.Commands;
using ReflectionExample.Dtos;

namespace ReflectionExample.Events.Events
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TCommand"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="Comanda"></param>
    /// <param name="Result"></param>
    public record BaseEvent<TCommand, TResult>(TCommand Command, TResult Result) : INotification where TCommand : IRequest<TResult>;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="command"></param>
    public record CommandExample1UnitEvent(CommandExample1Unit Command, Unit Result) : BaseEvent<CommandExample1Unit, Unit>(Command, Result);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Command"></param>
    /// <param name="Result"></param>
    public record CommandExample1UnitEvent2(CommandExample1Unit Command, Unit Result) : BaseEvent<CommandExample1Unit, Unit>(Command, Result);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Command"></param>
    /// <param name="Result"></param>
    public record CommandExample1Event(CommandExample1 Command, Output1 Result) : BaseEvent<CommandExample1, Output1>(Command, Result);
}
