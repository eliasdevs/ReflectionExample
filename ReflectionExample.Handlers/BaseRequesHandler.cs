using MediatR;
using ReflectionExample.Handlers.Events;
using System.Reflection;

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

            var assembly = Assembly.GetExecutingAssembly();
            var createCustomersType = request.GetType();

            var baseEventType = typeof(BaseEvent<,>);
            // Filtrar los tipos que coincidan tanto en Command como en Result
            var matchingTypes = assembly.GetTypes()
                .Where(t => t.BaseType != null && t.BaseType.IsGenericType
                            && t.BaseType.GetGenericTypeDefinition() == baseEventType)
                .Where(t => t.GetProperty(nameof(BaseEvent<TCommand, TResult>.Command))?.PropertyType == createCustomersType
                            && t.GetProperty(nameof(BaseEvent<TCommand, TResult>.Result))?.PropertyType == result?.GetType())
                .ToList(); // Obtener la lista de tipos coincidentes

            // Aquí puedes manejar los tipos coincidentes como desees
            foreach (var targetType in matchingTypes)
            {
                // Crear la instancia del tipo de evento pasando el comando y el resultado
                var instance = Activator.CreateInstance(targetType, request, result);

                // Publicar el evento usando Mediator
                if (instance is INotification notification)
                {
                    await _mediator.Publish(notification, cancellationToken);
                }
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
