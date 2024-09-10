using MediatR;
using ReflectionExample.Handlers.Events;

namespace ReflectionExample.Handlers.EventsHandler
{
    internal class CommandExample1UnitEventHandler : INotificationHandler<CommandExample1UnitEvent>, INotificationHandler<CommandExample1UnitEvent2>, INotificationHandler<CommandExample1Event>
    {
        public Task Handle(CommandExample1UnitEvent notification, CancellationToken cancellationToken)
        {
            Console.WriteLine("salida de " + nameof(CommandExample1UnitEvent));
            return Task.CompletedTask;
        }

        public Task Handle(CommandExample1UnitEvent2 notification, CancellationToken cancellationToken)
        {
            Console.WriteLine("salida de " + nameof(CommandExample1UnitEvent2));
            return Task.CompletedTask;
        }

        public Task Handle(CommandExample1Event notification, CancellationToken cancellationToken)
        {
            Console.WriteLine("salida de " + nameof(CommandExample1Event));
            return Task.CompletedTask;
        }
    }
}
