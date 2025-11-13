using FluentValidation;
using CMS.Application.Interfaces.Messaging;

namespace CMS.Infrastructure.Messaging.Pipline
{
    public class ValidationBehavior<TRequest, TResponse> : IAppPipelineBehavior<TRequest, TResponse>
    {
        private readonly IValidator<TRequest> _validator;

        public ValidationBehavior(IValidator<TRequest> validator)
        {
            _validator = validator;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            Func<Task<TResponse>> next,
            CancellationToken cancellationToken = default)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            return await next();
        }
    }
}
