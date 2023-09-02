using MediatRLibrary;
using System.Reflection;

namespace CustomMediatR.Utilities;

public class Mediator : IMediator
{
    public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        Type requestHandlerType = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));
        Assembly assembly = Assembly.Load(Memory.assembly);
        var implementingType = assembly.GetTypes().FirstOrDefault(t => t.GetInterfaces().Contains(requestHandlerType));

        var type = Activator.CreateInstance(implementingType, null);

        var handleMethod = requestHandlerType.GetMethod("Handle");
        var response = await (Task<TResponse>)handleMethod.Invoke(type, new object[] { request, cancellationToken });

        return response;
    }
}



