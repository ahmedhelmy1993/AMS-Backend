using System.Collections.ObjectModel;

namespace AMS.Domain._SharedKernel.DTOs.Response
{
    public class ValidatableResponse
    {
        private readonly IList<string> _errorMessages;

        public ValidatableResponse(IList<string> errors = null)
        {
            _errorMessages = errors ?? new List<string>();
        }

        public bool IsValidResponse => !_errorMessages.Any();
        public IReadOnlyCollection<string> Errors => new ReadOnlyCollection<string>(_errorMessages);
    }

    public class ValidatableResponse<TModel> : ValidatableResponse
    where TModel : class
    {
        public ValidatableResponse(TModel model, IList<string> validationErrors = null)
            : base(validationErrors)
        {
            Result = model;
        }

        public TModel Result { get; }
    }
}
