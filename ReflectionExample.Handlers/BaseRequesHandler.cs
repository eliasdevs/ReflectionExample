using MediatR;

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
            var result = await ExecuteAsync(request, cancellationToken);
            await _mediator.Publish(request, cancellationToken);
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
