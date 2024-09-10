using MediatR;
using ReflectionExample.Events;
using ReflectionExample.Events.Events;

namespace ReflectionExample.Handlers
{
    public abstract class BaseRequesHandler<TCommand, TResult> : IRequestHandler<TCommand, TResult> where TCommand : IRequest<TResult>
    {
        private readonly IMediator _mediator;
        public BaseRequesHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public abstract Task<TResult> ExecuteAsync(TCommand request, CancellationToken cancellationToken);

        public async Task<TResult> Handle(TCommand request, CancellationToken cancellationToken)
        {
            TResult result = await ExecuteAsync(request, cancellationToken);

            var assembly = typeof(EventsMarker).Assembly;

            var baseEventType = typeof(BaseEvent<,>);
            // Filtrar los tipos que coincidan tanto en Command como en Result
            List<Type> matchingTypes = assembly.GetTypes()
                .Where(t => t.BaseType != null && t.BaseType.IsGenericType &&
                            t.BaseType.GetGenericTypeDefinition() == baseEventType &&
                            t.GetProperty(nameof(BaseEvent<TCommand, TResult>.Command))?.PropertyType == request.GetType() &&
                            t.GetProperty(nameof(BaseEvent<TCommand, TResult>.Result))?.PropertyType == result?.GetType())
                .ToList();

            foreach (var targetType in matchingTypes)
            {
                int parameterCount = targetType.GetConstructors().Single().GetParameters().Length;

                object? instance = parameterCount switch
                {
                    1 => Activator.CreateInstance(targetType, request),
                    2 => Activator.CreateInstance(targetType, request, result),
                    _ => throw new InvalidOperationException($"El constructor de {targetType.FullName} no es compatible con la cantidad de parámetros esperada.")
                };

                // Publicar el evento usando Mediator
                if (instance is INotification notification)
                    await _mediator.Publish(notification, cancellationToken);
            }

            return result;
        }
    }
    /// <summary>
    /// version que no retorna nada
    /// </summary>
    /// <typeparam name="TCommand"></typeparam>
    public abstract class BaseRequesHandler<TCommand> : BaseRequesHandler<TCommand, Unit> where TCommand : IRequest<Unit>
    {
        public BaseRequesHandler(IMediator mediator) : base(mediator) { }
    }
}
