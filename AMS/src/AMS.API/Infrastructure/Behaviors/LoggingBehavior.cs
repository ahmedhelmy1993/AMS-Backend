using MediatR;
using Serilog;

namespace AMS.API.Infrastructure.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : MediatR.IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request,  CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            Log.Information("----- Handling command {CommandName} ({@Command})", request.GetGenericTypeName(), request);
            TResponse response = default;
            try
            {
                response = await next();
                Log.Information("----- Command {CommandName} handled - response: {@Response}", request.GetGenericTypeName(), response);
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, ex.Message);
                throw;
            }
            return response;
        }
    }
}
