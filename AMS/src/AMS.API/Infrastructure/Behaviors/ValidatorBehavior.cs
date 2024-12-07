using AMS.Domain._SharedKernel.DTOs.Response;
using FluentValidation;
using MediatR;
using Serilog;

namespace AMS.API.Infrastructure.Behaviors
{
    public class ValidatorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : MediatR.IRequest<TResponse>
    {
        private readonly IValidator<TRequest>[] _validators;

        public ValidatorBehavior(IValidator<TRequest>[] validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request,  CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var typeName = request.GetGenericTypeName();

            Log.Information("----- Validating command {CommandType}", typeName);

            var failures = _validators
                .Select(v => v.Validate(request))
                .SelectMany(result => result.Errors)
                .Where(error => error != null)
                .ToList();

            if (failures.Any())
            {
                Log.Information("Validation errors - {CommandType} - Command: {@Command} - Errors: {@ValidationErrors}", typeName, request, failures);
                var errorList = failures.Select(a => a.ErrorMessage).ToList();

                var responseType = typeof(TResponse);

                if (responseType.IsGenericType)
                {
                    var invalidResponseType = typeof(ValidatableResponse<>).MakeGenericType(responseType.GetGenericArguments()[0]);
                    return (TResponse)Activator.CreateInstance(invalidResponseType, null, errorList);
                }
            }

            return await next();
        }
    }
}

