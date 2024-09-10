using MediatR;
using ReflectionExample.Dtos;

namespace ReflectionExample.Api.Commands
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Data1"></param>
    public record CommandExample1Unit(CommandData1 Data1) : IRequest;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Data1"></param>
    public record CommandExample1(CommandData1 Data1) : IRequest<Output1>;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Data2"></param>
    public record CommandExample2(CommandData2 Data2) : IRequest<Output2>;
}
