using MediatR;
using ReflectionExample.Commands;
using ReflectionExample.Dtos;

namespace ReflectionExample.Handlers.Commands
{
    internal class CommandExample1Handler : BaseRequesHandler<CommandExample1, Output1>
    {
        public CommandExample1Handler(IMediator mediator) : base(mediator)
        {
        }
        public override Task<Output1> ExecuteAsync(CommandExample1 request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new Output1("Prueba de salida"));
        }
    }
}
