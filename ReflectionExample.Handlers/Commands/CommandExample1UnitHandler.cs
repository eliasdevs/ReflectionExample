using MediatR;
using ReflectionExample.Commands;

namespace ReflectionExample.Handlers.Commands
{
    internal class CommandExample1UnitHandler : BaseRequesHandler<CommandExample1Unit>
    {
        public CommandExample1UnitHandler(IMediator mediator) : base(mediator)
        {
        }

        public override Task<Unit> ExecuteAsync(CommandExample1Unit request, CancellationToken cancellationToken)
        {
            Console.WriteLine("Pr");
            return Task.FromResult(Unit.Value);
        }
    }
}
